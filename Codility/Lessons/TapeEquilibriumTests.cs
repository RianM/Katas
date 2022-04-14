using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class TapeEquilibriumTests
    {
        [TestCase(new int[] { 3, 1, 2, 4, 3 }, 1)]
        [TestCase(new int[] { 1, 10 }, 9)]
        [TestCase(new int[] { -1000, 1000 }, 2000)]
        public void Tests(int[] A, int expectedResult)
        {
            var result = TapeEquilibrium(A);

            result.Should().Be(expectedResult);
        }

        public int TapeEquilibrium(int[] A)
        {
            int sum = A.Sum();

            int min = int.MaxValue;

            int partA = 0;
            int partB = 0;
            int diff = 0;

            for (int i = 1; i < A.Length; i++)
            {
                partA += A[i - 1];
                partB = sum - partA;
                diff = Math.Abs(partA - partB);

                if (diff < min)
                {
                    min = diff;
                }
            }

            return min;
        }
    }
}