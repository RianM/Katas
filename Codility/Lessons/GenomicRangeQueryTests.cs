using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class GenomicRangeQueryTests
    {
        [TestCase("CAGCCTA", new int[] { 2, 5, 0 }, new int[] { 4, 5, 6 }, new int[] { 2, 4, 1 })]
        public void Tests(string S, int[] P, int[] Q, int[] expectedResult)
        {
            var result = GenomicRangeQuery(S, P, Q);

            result.Should().BeEquivalentTo(expectedResult, option => option.WithStrictOrdering());
        }

        // TODO - O(n x m) // Brute force, you can do better
        public int[] GenomicRangeQuery(string S, int[] P, int[] Q)
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
                for (int j =P[i]; j <= Q[i]; j++)
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
    }
}