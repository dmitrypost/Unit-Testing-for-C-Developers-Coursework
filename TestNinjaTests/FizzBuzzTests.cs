using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinjaTests
{
    public class FizzBuzzTests
    {
        [TestCase(45)]
        [TestCase(30)]
        [TestCase(15)]
        public void GetOutput_GivenNumberDivisibleBy3And5_ReturnsFizzBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result == "FizzBuzz");
        }
        
        [TestCase(48)]
        [TestCase(3)]
        [TestCase(12)]
        public void GetOutput_GivenNumberDivisibleBy3ButNot5_ReturnsFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result == "Fizz");
        }

        
        [TestCase(50)]
        [TestCase(5)]
        [TestCase(20)]
        public void GetOutput_GivenNumberDivisibleBy5ButNot3_ReturnsBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result == "Buzz");
        }

        [TestCase(52)]
        [TestCase(7)]
        [TestCase(22)]
        public void GetOutput_GivenNumberNotDivisibleBy3Or5_ReturnsNumber(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result == $"{number}");
        }


    }
}
