using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinjaTests
{
    public class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_SendIdZero_ReturnsNotFound()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(0);

            Assert.That(result, Is.TypeOf<NotFound>());
        }
        
        [TestCase(1)]
        [TestCase(99)]
        public void GetCustomer_SendNonZero_ReturnsOk(int id)
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(id);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
