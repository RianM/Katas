using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class CountDivTests
    {
        [TestCase(6, 11, 2, 3)]
        public void Tests(int A, int B, int K, int expectedResult)
        {
            var result = CountDiv(A, B, K);

            result.Should().Be(expectedResult);
        }

        public int CountDiv(int A, int B, int K)
        {
            // calculate the number of times B is divisible by K
            // calculate the number of times A is divisible by K
            // Subtract A/K from B/K aka B/K - A/K
            // Add 1 to the edge case of the value of A being divisible by K
            
            return B / K - A / K + (A % K == 0 ? 1 : 0);
        }
    }
}