using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public ActionResult DeleteEmployee(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null) return new NotFoundResult();
            _repository.Delete(employee);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class NotFoundResult : ActionResult { }
    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }

    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        void Delete(Employee employee);
        IEnumerable<Employee> GetAll();
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IEmployeeContext _context;

        public EmployeeRepository(IEmployeeContext context)
        {
            _context = context;
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Find(id);
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
    }
    
    public interface IEmployeeContext
    {
        DbSet<Employee> Employees { get; set; }
        void SaveChanges();
    }

    public class EmployeeContext : IEmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}