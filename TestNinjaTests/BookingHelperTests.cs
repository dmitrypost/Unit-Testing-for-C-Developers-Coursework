using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinjaTests
{
    public class BookingHelperTests
    {
        private IBookingRepository _repository;
        
        private static readonly IList<Booking> ExistingBookings = new List<Booking>(){
            new Booking
            {
                Id = 1, 
                ArrivalDate = ArriveOn(8,30,2020), 
                DepartureDate = DepartOn(9,4,2020),
                Reference = "a",
            },
            new Booking
            {
                Id = 2, 
                ArrivalDate = ArriveOn(1,15,2020), 
                DepartureDate = DepartOn(1,20,2020),
                Reference = "b",
                Status = "Cancelled"
            }
        };

        private static DateTime ArriveOn(int month, int day, int year) => DateTime.Parse($"{month}/{day}/{year} 2:00 PM");
        
        private static DateTime DepartOn(int month, int day, int year) => DateTime.Parse($"{month}/{day}/{year} 10:00 AM");

        private static DateTime DaysBefore(DateTime date, int days = -1) => date.AddDays(days);
        
        private static DateTime DaysAfter(DateTime date, int days = 1) => date.AddDays(days);

        [SetUp]
        public void Setup()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            _repository = new BookingRepository(unitOfWork.Object);

            unitOfWork.Setup(x => x.Query<Booking>()).Returns(ExistingBookings.AsQueryable);
        }

        [Test]
        public void OverlappingBookingsExist_StartsAndFinishesBeforeExistingBooking_ReturnEmptyString()
        {
            //Arrange
            var booking = new Booking
            {
                Id = 3,
                ArrivalDate = DaysBefore(ExistingBookings[0].ArrivalDate, 2),
                DepartureDate = DaysBefore(ExistingBookings[0].ArrivalDate),
            };

            //Act
            var result = BookingHelper.OverlappingBookingsExist(_repository, booking);

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnsBookingReference()
        {
            //Arrange
            var booking = new Booking
            {
                Id = 3,
                ArrivalDate = DaysBefore(ExistingBookings[0].ArrivalDate),
                DepartureDate = DaysAfter(ExistingBookings[0].ArrivalDate),
            };

            //Act
            var result = BookingHelper.OverlappingBookingsExist(_repository, booking);

            //Assert
            Assert.That(result, Is.EqualTo(ExistingBookings[0].Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesAfterExistingBooking_ReturnsBookingReference()
        {
            //Arrange
            var booking = new Booking
            {
                Id = 3,
                ArrivalDate = DaysBefore(ExistingBookings[0].ArrivalDate),
                DepartureDate = DaysAfter(ExistingBookings[0].DepartureDate),

            };

            //Act
            var result = BookingHelper.OverlappingBookingsExist(_repository, booking);

            //Assert
            Assert.That(result, Is.EqualTo(ExistingBookings[0].Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesInTheMiddleOfExistingBooking_ReturnsBookingReference()
        {
            //Arrange
            var booking = new Booking
            {
                Id = 3,
                ArrivalDate = DaysAfter(ExistingBookings[0].ArrivalDate),
                DepartureDate = DaysBefore(ExistingBookings[0].DepartureDate),

            };

            //Act
            var result = BookingHelper.OverlappingBookingsExist(_repository, booking);

            //Assert
            Assert.That(result, Is.EqualTo(ExistingBookings[0].Reference));
        }
        
        [Test]
        public void OverlappingBookingsExist_BookingStartsInTheMiddleOfExistingBookingButFinishesAfter_ReturnsBookingReference()
        {
            //Arrange
            var booking = new Booking
            {
                Id = 3,
                ArrivalDate = DaysAfter(ExistingBookings[0].ArrivalDate),
                DepartureDate = DaysAfter(ExistingBookings[0].DepartureDate),

            };

            //Act
            var result = BookingHelper.OverlappingBookingsExist(_repository, booking);

            //Assert
            Assert.That(result, Is.EqualTo(ExistingBookings[0].Reference));
        }
        
        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesAfterExistingBooking_ReturnsBookingReference()
        {
            //Arrange
            var booking = new Booking
            {
                Id = 3,
                ArrivalDate = DaysAfter(ExistingBookings[0].DepartureDate),
                DepartureDate = DaysAfter(ExistingBookings[0].DepartureDate, 2),

            };

            //Act
            var result = BookingHelper.OverlappingBookingsExist(_repository, booking);

            //Assert
            Assert.That(result, Is.Empty);
        }       
        
        [Test]
        public void OverlappingBookingsExist_CancelledBookingStartsInTheMiddleOfExistingBooking_ReturnsBookingReference()
        {
            //Arrange
            var booking = new Booking
            {
                Id = 3,
                ArrivalDate = DaysAfter(ExistingBookings[0].ArrivalDate),
                DepartureDate = DaysAfter(ExistingBookings[0].DepartureDate),
                Status = "Cancelled"
            };

            //Act
            var result = BookingHelper.OverlappingBookingsExist(_repository, booking);

            //Assert
            Assert.That(result, Is.Empty);
        }
    }
}