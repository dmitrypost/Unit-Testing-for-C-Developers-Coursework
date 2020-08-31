using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinjaTests
{
    public class DemeritPointsCalculatorTests
    {
        [TestCase(-5)]
        [TestCase(305)]
        public void CalculateDemeritPoints_GivenOutOfRangeSpeed_ExpectsArgumentOutOfRangeException(int speed)
        {
            var calculator = new DemeritPointsCalculator();

            Assert.Catch<ArgumentOutOfRangeException>(() => calculator.CalculateDemeritPoints(speed));
        }

        [TestCase(5,0)]
        [TestCase(50,0)]
        [TestCase(65,0)]
        [TestCase(67,0)]
        [TestCase(70,1)]
        [TestCase(75,2)]
        [TestCase(78,2)]
        public void CalculateDemeritPoints_GivenSpeed_ExpectsPoints(int speed, int pointsExpected)
        {
            var calculator = new DemeritPointsCalculator();

            var results = calculator.CalculateDemeritPoints(speed);

            Assert.That(results,Is.EqualTo(pointsExpected));
        }
    }
}
