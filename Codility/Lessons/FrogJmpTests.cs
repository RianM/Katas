using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class FrogJmpTests
    {
        [TestCase(10,85,30,3)]
        public void Tests(int x, int y, int d, int expectedResult)
        {
            var result = FrogJmp(x,y,d);

            result.Should().Be(expectedResult);
        }

        public int FrogJmp(int X, int Y, int D)
        {
            if (X == Y)
                return 0;

            int distance = Y - X;
            int jumps = distance / D + ( distance % D > 0 ? 1 : 0);
            return jumps;
        }
    }
}