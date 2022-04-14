using System;
using NUnit.Framework;

namespace Hacking
{
    public class MissingIntegerTests
    {
        [TestCase(new int[] { 1, 3, 6, 4, 1, 2 }, 5)]
        [TestCase(new int[] { 1, 2, 3 }, 4)]
        [TestCase(new int[] { -1, -3 }, 1)]
        public void MissingIntegerTest(int[] A, int expectedResult)
        {
            var result = MissingInteger(A);
            Assert.True(result == expectedResult, $"Expected {expectedResult} but got {result}");
        }

        public int MissingInteger(int[] A)
        {
            Array.Sort(A);
            int min = 1;

            foreach (int num in A)
            {
                if (num > min)
                {
                    break;
                }

                if (num == min)
                {
                    min++;
                }
            }

            return min;
        }
    }
}