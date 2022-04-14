using System;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class PermCheckTests
    {
        [TestCase(new int[] {4,1,3,2},1)]
        [TestCase(new int[] {4,1,3},0)]
        [TestCase(new int[] {1,1000000000},0)]
        [TestCase(new int[] {1,2},1)]
        [TestCase(new int[] {1,3},0)]
        public void Tests(int[] A, int expectedResult)
        {
            var result = PermCheck(A);

            result.Should().Be(expectedResult);
        }

        public int PermCheck(int[] A)
        {
            if (A.Length == 0)
            {
                return 0;
            }
            
            Array.Sort(A);

            for (int i = 0; i < A.Length; i++)
            {
                // Since it has to start at one we can check that the value is 1 more than the index
                if (A[i] != i+1)
                {
                    return 0;
                }
            }

            return 1;
        }
    }
}