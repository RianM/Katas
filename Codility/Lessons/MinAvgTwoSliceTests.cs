using System;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    public class MinAvgTwoSliceTests
    {
        private static object[][] _standardTest = new object[][]
        {
            new object[] { new int[] { 4, 2, 2, 5, 1, 5, 8 }, 1 }
        };

        [TestCaseSource(nameof(_standardTest))]
        public void Test_Brute(int[] A, int expectedResult)
        {
            var result = MinAvgTwoSlice_Brute(A);
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(_standardTest))]
        public void Test_Efficient(int[] A, int expectedResult)
        {
            var result = MinAvgTwoSlice_Efficient(A);
            result.Should().Be(expectedResult);
        }

        int MinAvgTwoSlice_Brute(int[] A)
        {
            double minValue = double.MaxValue;
            int minPos = 0;

            for (int i = 0; i < A.Length; i++)
            {
                for (int j = i + 1; j < A.Length; j++)
                {
                    var cur = GetSliceAverage(i, j);
                    if (cur < minValue)
                    {
                        minValue = cur;
                        minPos = i;
                    }
                }
            }

            return minPos;

            double GetSliceAverage(int i, int j)
            {
                int count = j - i + 1;
                double sum = 0;
                for (int k = i; k <= j; k++)
                {
                    sum += A[k];
                }

                return sum / count;
            }
        }

        int MinAvgTwoSlice_Efficient(int[] A)
        {
            double min = Int32.MaxValue;
            int minPos = -1;

            for (int i = 0; i < A.Length - 2; i++)
            {
                var avg1 = Average(A[i], A[i + 1], A[i + 2]);
                var avg2 = Average(A[i], A[i + 1]);
                var cur = Math.Min(avg1, avg2);

                if (cur < min)
                {
                    min = cur;
                    minPos = i;
                }
            }
            
            var avg3 = Average(A[A.Length-2], A[A.Length-1]);

            if (avg3 < min)
            {
                minPos = A.Length-2;
            }

            return minPos;

            double Average(params int[] values)
            {
                int sum = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    sum += values[i];
                }

                return (double)sum / values.Length;
            }
        }
    }
}