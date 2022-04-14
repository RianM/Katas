using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class CyclicRotationTests
    {
        [TestCase(new int[] { 3, 8, 9, 7, 6 }, 1, new int[] { 6, 3, 8, 9, 7 })]
        [TestCase(new int[] { 3, 8, 9, 7, 6 }, 3, new int[] { 9, 7, 6, 3, 8 })]
        [TestCase(new int[] { 0, 0, 0 }, 1, new int[] { 0, 0, 0 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4, new int[] { 1, 2, 3, 4 })]
        public void Tests(int[] A, int K, int[] expectedResult)
        {
            var result = CyclicRotation(A, K);

            result.Should().BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
        }

        public int[] CyclicRotation(int[] A, int K)
        {
            int max = A.Length;
            return A.Select((s, i) => A[RotatedIndex(i, max, K)]).ToArray();
        }

        int RotatedIndex(int index, int max, int moves)
        {
            int safeMoves = moves % max;
            int newIndex = index - safeMoves;
            if (newIndex < 0)
            {
                newIndex += max;
            }

            return newIndex;
        }
    }
}