using FluentAssertions;
using NUnit.Framework;

namespace Bowling.Tests
{
    public class BowlingScoreCalculatorTests
    {
        [TestCase("X X X X X X X X X X X X", 300)]
        [TestCase("X X X X X X X X X X X -", 290)]
        [TestCase("X X X X X X X X X X X 5", 295)]
        [TestCase("X X X X X X X X X X --", 270)]
        [TestCase("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-", 90)]
        [TestCase("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5", 150)]
        [TestCase("11 11 11 11 11 11 11 11 11 11", 20)]
        [TestCase("11 11 11 11 11 11 11 11 11 1/1", 29)]
        [TestCase("11 11 11 11 11 11 11 11 11 X 11", 30)]
        [TestCase("11 11 11 11 11 11 11 11 11 X X 1", 39)]
        [TestCase("11 11 11 11 11 11 11 11 11 X 1/", 38)]
        public void CalculateScores(string rolls,
            int expectedScore)
        {
            BowlingScoreCalculator calculator = new();

            var score = calculator.CalculateScore(rolls);

            score.Should().Be(expectedScore);
        }

        [TestCase("--", null, 10)]
        [TestCase("11", null, 12)]
        [TestCase("1/", "--", 20)]
        [TestCase("1/", "11", 20)]
        [TestCase("1/", "1/", 20)]
        [TestCase("1/", "X", 20)]
        [TestCase("X", "--", 20)]
        [TestCase("X", "-1", 20)]
        [TestCase("X", "1-", 21)]
        [TestCase("X", "1/", 21)]
        [TestCase("X", "X", 30)]
        public void CalculateStrike(string nextPair,
            string secondNextPair,
            int expectedScore)
        {
            BowlingScoreCalculator calculator = new();

            var score = calculator.CalculateStrike(nextPair, secondNextPair);

            score.Should().Be(expectedScore);
        }

        [TestCase("1/", "1-", 11)]
        [TestCase("1/", "--", 10)]
        [TestCase("1/", "-/", 10)]
        [TestCase("1/", "X", 20)]
        [TestCase("1/1", null, 11)]
        [TestCase("1/-", null, 10)]
        [TestCase("1/X", null, 20)]
        public void CalculateSpare(string pair,
            string nextPair,
            int expectedScore)
        {
            BowlingScoreCalculator calculator = new();

            var score = calculator.CalculateSpare(pair, nextPair);

            score.Should().Be(expectedScore);
        }
        
        [TestCase('-',0)]
        [TestCase('1',1)]
        [TestCase('X',10)]
        public void CalculateSimpleScore_SingleCharacter(char value,
            int expectedScore)
        {
            BowlingScoreCalculator calculator = new();

            var score = calculator.CalculateSimpleScore(value);

            score.Should().Be(expectedScore);
        }
        
        [TestCase(null, 0)]
        [TestCase("--", 0)]
        [TestCase("1-", 1)]
        [TestCase("-1", 1)]
        [TestCase("X", 10)]
        public void CalculateSimpleScore_Pair(string? pair,
            int expectedScore)
        {
            BowlingScoreCalculator calculator = new();

            var score = calculator.CalculateSimpleScore(pair);

            score.Should().Be(expectedScore);
        }
        
        [TestCase(null, 0,0)]
        [TestCase(null, 1,0)]
        [TestCase("--", 0,0)]
        [TestCase("--", 1,0)]
        [TestCase("1-", 0,1)]
        [TestCase("1-", 1,0)]
        [TestCase("-1", 0,0)]
        [TestCase("-1", 1,1)]
        [TestCase("X", 0,10)]
        [TestCase("X", 1,0)]
        public void CalculateSimpleScore_PairANDTurn(string? pair, int turn,
            int expectedScore)
        {
            BowlingScoreCalculator calculator = new();

            var score = calculator.CalculateSimpleScore(pair, turn);

            score.Should().Be(expectedScore);
        }
    }
}