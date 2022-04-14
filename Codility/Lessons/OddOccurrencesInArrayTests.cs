using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class OddOccurrencesInArrayTests
    {
        [TestCase(new int[] { 9, 3, 9, 3, 9, 7, 9 }, 7)]
        public void Tests(int[] A, int expectedResult)
        {
            var result = OddOccurrencesInArray(A);

            result.Should().Be(expectedResult);
        }

        public int OddOccurrencesInArray(int[] A)
        {
            return A.GroupBy(gb => gb)
                .Where(w => w.Count() % 2 == 1)
                .Select(s => s.Key)
                .FirstOrDefault();
        }
    }
}