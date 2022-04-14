using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class MaxCountersTests
    {
        [TestCase(5, new int[] { 3, 4, 4, 6, 1, 4, 4 }, new int[] { 3, 2, 2, 4, 2 })]
        public void Tests(int N, int[] A, int[] expectedResult)
        {
            var result = MaxCounters(N, A);

            result.Should().BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
        }

        public int[] MaxCounters(int N, int[] A)
        {
            int[] counters = new int[N];

            int maxOperationValue = N + 1;
            int maxValue = 0;
            int minValue = 0;
            
            foreach (int i in A)
            {
                // If we should do the max operation, do it and continue the loop.
                if (i == maxOperationValue)
                {
                    minValue = maxValue;
                    continue;
                }
            
                // Else, do the addition operation
                if (counters[i - 1] < minValue)
                {
                    counters[i - 1] = minValue + 1;
                }
                else
                {
                    counters[i - 1]++;
                }
                
                if (counters[i - 1] > maxValue)
                {
                    maxValue = counters[i - 1];
                }
            }
            
            for (int j = 0; j < N; j++)
            {
                if (counters[j] < minValue)
                {
                    counters[j] = minValue;
                }
            }

            return counters;
        }
    }
}