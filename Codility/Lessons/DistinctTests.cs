using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using TestHelpers;

namespace Hacking
{
    [TestFixture]
    public class DistinctTests
    {
        private static object[][] _standardTest = new object[][]
        {
            new object[] { new int[] { 2, 1, 1, 2, 3, 1 }, 3 }
        };

        [TestCaseSource(nameof(_standardTest))]
        public void Test_Brute(int[] A, int expectedResult)
        {
            var result = Distinct_Brute(A);
            result.Should().Be(expectedResult);
        }

        public static IEnumerable<object[]> GenerateTestCases()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 3; i++)
            {
                var n = fixture.CreateInt(0, 100000);

                int[] a = new int[n];

                for (int j = 0; j < n; j++)
                {
                    a[j] = fixture.CreateInt(-1000000, 1000000);
                }

                yield return new object[] { a, Distinct_Brute(a) };
            }
        }

        [TestCaseSource(nameof(GenerateTestCases))]
        public void Distinct_Brute_GeneratedTests(int[] A, int expectedResult)
        {
            var result = Distinct_Brute(A);

            result.Should().Be(expectedResult);
        }

        static int Distinct_Brute(int[] A)
        {
            if (A.Length == 0)
            {
                return 0;
            }

            int count = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < A.Length; i++)
            {
                if (!dic.ContainsKey(A[i]))
                {
                    dic.Add(A[i], 1);
                    count++;
                }
            }

            return count;
        }
    }
}