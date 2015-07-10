using System;
using NUnit.Framework;

namespace BookHelper.Tests
{
    [TestFixture]
    public class PagesRangeTests
    {
        [Test]
        [TestCase(-5, 7)] // Documentation about TestCase attribute: http://www.nunit.org/index.php?p=testCase&r=2.5
        [TestCase(-7, -5)]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctr_When_Negative_pages_passed_Then_throws_exception(int from, int to)
        {
            // Act
            new PagesRange(from, to);
        }

        // TODO 1: Write test that checks that "from" should be less or equal than "to". Fix the code if test fails.
        [Test]
        [TestCase(9, 4)]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctr_When_From_less_to_Then_throws_exception(int from, int to)
        {
            // Act
            new PagesRange(from, to);
        }
        [Test]
        [TestCase(5, 5)]
        public void Ctr_When_From_equal_to_Then_all_correct(int from, int to)
        {
            // Act
            new PagesRange(from, to);
        }
    }
}