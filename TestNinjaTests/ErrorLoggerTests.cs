using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinjaTests
{
    public class ErrorLoggerTests
    {

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void Log_GivenNullOrWhiteSpace_ExpectsArgumentNullExceptionThrown(string error)
        {
            var logger = new ErrorLogger();

            
            Assert.Throws<ArgumentNullException>(() => logger.Log(error));
        }

        [TestCase("error occurred")]
        public void Log_GivenErrorMessage_ExpectsErrorMessage(string errorMessage)
        {
            var logger = new ErrorLogger();

            logger.Log(errorMessage);

            Assert.IsTrue(logger.LastError.Equals(errorMessage));
        }

        [TestCase("error occurred")]
        public void Log_GivenErrorMessage_RaisesEvent(string errorMessage)
        {
            var logger = new ErrorLogger();

            var guid = Guid.Empty;
            logger.ErrorLogged += delegate(object? sender, Guid e) { guid = e; };
            logger.Log(errorMessage);

            Assert.That(guid != Guid.Empty);
        }
    }
}
