using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(IBookingRepository repository, Booking booking)
        {
            if (booking.Status == "Cancelled") return string.Empty;

            var activeBookings = repository.GetActiveBookings(booking.Id);
            
            var overlappingBooking = activeBookings.GetOverlappingBooking(booking);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public static class QueryableBookingExtensions
    {
        public static Booking GetOverlappingBooking(this IQueryable<Booking> bookings, Booking booking)
        {
            return bookings.FirstOrDefault(b => 
                booking.ArrivalDate < b.DepartureDate && b.ArrivalDate < booking.DepartureDate);
        }
    }

    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludeBookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Booking> GetActiveBookings(int? excludeBookingId = null)
        {
            var bookings = _unitOfWork.Query<Booking>().Where(b => b.Status != "Cancelled");

            return excludeBookingId.HasValue ? bookings.Where(x => x.Id != excludeBookingId.Value) : bookings;
        }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}