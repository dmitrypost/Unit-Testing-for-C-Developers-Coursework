using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinjaTests
{
    public class EmployeeControllerTests
    {
        private EmployeeController _controller;
        private Mock<IEmployeeRepository> _repository;
        private List<Employee> _employeesInDb;
        
        [SetUp]
        public void Setup()
        {
            _employeesInDb = new List<Employee>()
            {
                new Employee(), 
                new Employee(), 
                new Employee(), 
            };

            _repository = new Mock<IEmployeeRepository>();
            _controller = new EmployeeController(_repository.Object);
        }
        
        [TestCase(4)]
        [TestCase(7)]
        [TestCase(0)]
        public void DeleteEmployee_GivenInvalidInt_ReturnsNotFoundResult(int id)
        {
            var result = _controller.DeleteEmployee(id);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
        
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(1)]
        public void DeleteEmployee_GivenValidInt_CallsGetByIdAndDelete(int id)
        {
            _repository.Setup(x => x.GetById(id)).Returns(() => _employeesInDb[id]);
            
            _controller.DeleteEmployee(id);
            
            _repository.Verify(x => x.GetById(id));
            _repository.Verify(x=>x.Delete(It.IsAny<Employee>()));           
        }
        
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(1)]
        public void DeleteEmployee_GivenValidInt_ReturnsRedirectResult(int id)
        {
            _repository.Setup(x => x.GetById(id)).Returns(() => _employeesInDb[id]);
            
            var result = _controller.DeleteEmployee(id);
            
            Assert.That(result, Is.InstanceOf<RedirectResult>());        
        }
    }
}