using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class FrogRiverOneTests
    {
        [TestCase(5, new int[] { 1, 3, 1, 4, 2, 3, 5, 4 }, 6)]
        [TestCase(3, new int[] { 1, 3, 1, 3, 2, 1, 3 }, 4)]
        public void Tests(int x, int[] A, int expectedResult)
        {
            var result = FrogRiverOne(x, A);

            result.Should().Be(expectedResult);
        }

        public int FrogRiverOne(int X, int[] A)
        {
            var leaves = A.Select((pos, index) => new { pos, index })
                .GroupBy(gb => gb.pos)
                .Select(s => new { pos = s.Key, index = s.Min(m => m.index) })
                .OrderBy(ob => ob.pos)
                .ToList();

            int frogPos = 1;
            int maxSeconds = 0;

            foreach (var leaf in leaves)
            {
                // If there is a missing leaf, we can't make it
                if (leaf.pos > frogPos)
                {
                    return -1;
                }

                if (leaf.pos == frogPos)
                {
                    if (maxSeconds < leaf.index)
                    {
                        maxSeconds = leaf.index;
                    }

                    if (frogPos == X)
                    {
                        return maxSeconds;
                    }

                    frogPos++;
                }
            }

            return -1;
        }
    }
}