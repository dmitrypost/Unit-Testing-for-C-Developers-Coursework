using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinjaTests
{
    public class PhoneNumberTests
    {
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void Parse_GivenNullOrWhiteSpace_RaisesArgumentExceptionBlankNumber(string number)
        {
            Assert.Catch<ArgumentException>(() => PhoneNumber.Parse(number));
        }

        [TestCase("46516545")]
        [TestCase("")]
        [TestCase("48852")]
        public void Parse_GivenDifferentLengthsNumber_RaisesArgumentException(string number)
        {
            Assert.Catch<ArgumentException>(() => PhoneNumber.Parse(number));
        }

        [TestCase("8885554444","(888)555-4444")]
        public void Parse_GivenNumber_ToStringReturnsExpectedNumber(string number, string expected)
        {
            var phoneNumber = PhoneNumber.Parse(number);
            Assert.IsTrue(phoneNumber.ToString() == expected);
        }
    }
}
