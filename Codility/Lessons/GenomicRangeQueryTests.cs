using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using TestHelpers;

namespace Hacking
{
    [TestFixture]
    public class GenomicRangeQueryTests
    {
        [TestCase("CAGCCTA", new int[] { 2, 5, 0 }, new int[] { 4, 5, 6 }, new int[] { 2, 4, 1 })]
        public void GenomicRangeQuery_BruteForce_Tests(string S, int[] P, int[] Q, int[] expectedResult)
        {
            var result = GenomicRangeQuery_BruteForce(S, P, Q);

            result.Should().BeEquivalentTo(expectedResult, option => option.WithStrictOrdering());
        }

        [TestCase("CAGCCTA", new int[] { 2, 5, 0 }, new int[] { 4, 5, 6 }, new int[] { 2, 4, 1 })]
        public void GenomicRangeQuery_Efficient_Tests(string S, int[] P, int[] Q, int[] expectedResult)
        {
            var result = GenomicRangeQuery_Efficient(S, P, Q);

            result.Should().BeEquivalentTo(expectedResult, option => option.WithStrictOrdering());
        }
        
        public static IEnumerable<object[]> GenerateTestCases()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 3; i++)
            {
                var n = fixture.CreateInt(0, 100000);
                var m = fixture.CreateInt(0, 50000);
                int[] p = new int[m];
                int[] q = new int[m];

                var s = TestHelpers.TestHelpers.CreateString("ACGT", n);

                for (int j = 0; j < m; j++)
                {
                    p[j] = fixture.CreateInt(0, n - 1);
                    q[j] = fixture.CreateInt(p[j], n - 1);
                }

                yield return new object[] { s, p, q, GenomicRangeQuery_BruteForce(s, p, q) };
            }
        }
        
        [TestCaseSource(nameof(GenerateTestCases))]
        public void GenomicRangeQuery_Efficient_GeneratedTests(string S, int[] P, int[] Q, int[] expectedResult)
        {
            var result = GenomicRangeQuery_Efficient(S, P, Q);

            result.Should().BeEquivalentTo(expectedResult, option => option.WithStrictOrdering());
        }
        
        [TestCase("CAGCCTA", new int[] { 2, 5, 0 }, new int[] { 4, 5, 6 }, new int[] { 2, 4, 1 })]
        public void GenomicRangeQuery_Internet_Tests(string S, int[] P, int[] Q, int[] expectedResult)
        {
            var result = GenomicRangeQuery_Internet(S, P, Q);

            result.Should().BeEquivalentTo(expectedResult, option => option.WithStrictOrdering());
        }

        public static int[] GenomicRangeQuery_BruteForce(string S, int[] P, int[] Q)
        {
            int[] results = new int[P.Length];
            Dictionary<char, int> mapping = new Dictionary<char, int>()
            {
                { 'A', 1 },
                { 'C', 2 },
                { 'G', 3 },
                { 'T', 4 }
            };

            for (int i = 0; i < P.Length; i++)
            {
                int min = int.MaxValue;
                for (int j = P[i]; j <= Q[i]; j++)
                {
                    char key = S[j];
                    int val = mapping[key];
                    if (val < min)
                    {
                        min = val;
                    }
                }

                results[i] = min;
            }

            return results;
        }

        public int[] GenomicRangeQuery_Efficient(string S, int[] P, int[] Q)
        {
            int[] results = new int[P.Length];
            Dictionary<char, int> mapping = new Dictionary<char, int>()
            {
                { 'A', 1 },
                { 'C', 2 },
                { 'G', 3 },
                { 'T', 4 }
            };

            Dictionary<char, int[]> hash = new Dictionary<char, int[]>();

            foreach (var c in mapping.Keys)
            {
                hash.Add(c, new int[S.Length]);
                hash[c][0] = S[0] == c ? 1 : 0;
                for (int i = 1; i < S.Length; i++)
                {
                    var prev = hash[c][i - 1];
                    var inc = S[i] == c ? 1 : 0;
                    hash[c][i] =  prev+inc ;
                }
            }
 
            for (int i = 0; i < P.Length; i++)
            {
                int lPos = P[i];
                int rPos = Q[i];

                if (lPos == rPos)
                {
                    results[i] = mapping[S[lPos]];
                    continue;
                }

                foreach (var c in mapping.Keys)
                {
                    int l = hash[c][lPos];
                    int r = hash[c][rPos];
                    int occurrences = r - l;
                    if (occurrences > 0)
                    {
                        results[i] = mapping[c];
                        break;
                    }
                }
            }

            return results;
        }

        // Solution found on the internet - https://luisramalho.com/genomic-range-query
        public int[] GenomicRangeQuery_Internet(string s, int[] p, int[] q)
        {
            int[,] a = new int[4, s.Length]; // A, C, G and T

            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                switch (ch)
                {
                    case 'A':
                        a[0, i]++;
                        break;
                    case 'C':
                        a[1, i]++;
                        break;
                    case 'G':
                        a[2, i]++;
                        break;
                    case 'T':
                        a[3, i]++;
                        break;
                    default:
                        break;
                }
            }

            int[,] prefixSum = new int[4, s.Length + 1];
            for (int k = 1; k < s.Length + 1; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    prefixSum[j, k] = prefixSum[j, k - 1] + a[j, k - 1];
                }
            }

            int[] m = new int[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                int x = p[i];
                int y = q[i];
                for (int j = 0; j < 4; j++)
                {
                    if (prefixSum[j, y + 1] - prefixSum[j, x] > 0)
                    {
                        m[i] = j + 1;
                        break;
                    }
                }
            }

            return m;
        }
    }
}