using System;

namespace Bowling
{
    public class BowlingScoreCalculator
    {
        public int CalculateScore(string rolls)
        {
            int score = 0;
            var pairs = rolls.Split(" ");

            for (int i = 0; i < 10; i++)
            {
                // Strike
                if (pairs[i] == "X")
                {
                    string nextPair = GetPair(pairs, i + 1) ?? throw new InvalidOperationException();

                    string? secondNextPair = GetPair(pairs, i + 2);
                    
                    int strike = CalculateStrike(nextPair, secondNextPair);
                    score += strike;
                    continue;
                }

                string pair = GetPair(pairs, i) ?? throw new InvalidOperationException();
                
                if (pairs[i].Contains('/'))
                {
                    string? nextPair = GetPair(pairs, i + 1);
                    
                    int strike = CalculateSpare(pair, nextPair);
                    score += strike;
                    continue;
                }
                
                score += CalculateSimpleScore(pair);
            }

            return score;
        }

        internal string? GetPair(string[] pairs,
            int index)
        {
            if (pairs.Length > index)
            {
                return pairs[index];
            }

            return null;
        }

        internal int CalculateSpare(string pair, string? nextPair)
        {
            int spare = 10;

            // Bonus Spare
            if (pair.Length > 2)
            {
                return spare + CalculateSimpleScore(pair[2]);
            }

            if (nextPair != null)
            {
                return spare + CalculateSimpleScore(nextPair[0]);
            }

            return spare;
        }

        internal int CalculateStrike(string nextPair, string? secondNextPair)
        {
            int strike = 10;

            // Strike
            if (nextPair == "X")
            {
                strike += 10;
                if (secondNextPair == "X")
                {
                    strike += 10;
                }
                else
                {
                    strike += CalculateSimpleScore(secondNextPair, 0);
                }
            }
            // Spare
            else if (nextPair.Contains('/'))
            {
                strike += CalculateSpare(nextPair, null);
            }
            // Simple
            else
            {
                strike += CalculateSimpleScore(nextPair);
            }

            return strike;
        }
        
        internal int CalculateSimpleScore(string? pair)
        {
            int turnOne = CalculateSimpleScore(pair, 0);
            int turnTwo = CalculateSimpleScore(pair, 1);
            return turnOne + turnTwo;
        }

        internal int CalculateSimpleScore(string? pair, int turn)
        {
            // turn doesn't exist ( can happen in bonus round )
            if (pair == null || turn >= pair.Length)
            {
                return 0;
            }
            
            return CalculateSimpleScore(pair[turn]);
        }
        
        internal int CalculateSimpleScore(char value)
        {
            if (value == '-')
            {
                return 0;
            }

            if (value == 'X')
            {
                return 10;
            }
            
            return Int32.Parse(value.ToString());
        }
    }
}