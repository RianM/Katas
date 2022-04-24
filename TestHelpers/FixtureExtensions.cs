using AutoFixture;

namespace TestHelpers
{
    public static class FixtureExtensions
    {
        // Stolen from https://stackoverflow.com/questions/40811099/autofixture-for-number-ranges
        public static int CreateInt(this IFixture fixture, int min, int max)
        {
            return fixture.Create<int>() % (max - min + 1) + min;
        }
    }
}