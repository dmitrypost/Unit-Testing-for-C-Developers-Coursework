using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinjaTests
{
    [TestFixture]
    public class DateHelperTests
    {

        [TestCase("1/19/2020","2/1/2020")]
        [TestCase("2/8/2020","3/1/2020")]
        [TestCase("3/5/2020","4/1/2020")]
        [TestCase("4/2/2020","5/1/2020")]
        [TestCase("5/1/2020","6/1/2020")]
        [TestCase("6/9/2020","7/1/2020")]
        [TestCase("7/7/2020","8/1/2020")]
        [TestCase("8/12/2020","9/1/2020")]
        [TestCase("9/25/2020","10/1/2020")]
        [TestCase("10/17/2020","11/1/2020")]
        [TestCase("11/1/2020","12/1/2020")]
        [TestCase("12/5/2020","1/1/2021")]
        public void FirstOfNextMonth_GivenDate_ReturnsExpectedDate(DateTime date, DateTime dateExpected)
        {
            var result = DateHelper.FirstOfNextMonth(date);

            Assert.IsTrue(result.Equals(dateExpected));
        }
    }
}
