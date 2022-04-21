using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Problems
{
    public class RomanToInt
    {
        [TestCase("I", 1)]
        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("IV", 4)]
        [TestCase("V", 5)]
        [TestCase("VI", 6)]
        [TestCase("VII", 7)]
        [TestCase("VIII", 8)]
        [TestCase("IX", 9)]
        [TestCase("X", 10)]
        [TestCase("LVIII", 58)]
        [TestCase("MCMXCIV", 1994)]
        public void Solution1Tests(string s,
            int expectedResult)
        {
            int result = new Solution1().RomanToInt(s);

            result.Should().Be(expectedResult);
        }

        [TestCase("I", 1)]
        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("IV", 4)]
        [TestCase("V", 5)]
        [TestCase("VI", 6)]
        [TestCase("VII", 7)]
        [TestCase("VIII", 8)]
        [TestCase("IX", 9)]
        [TestCase("X", 10)]
        [TestCase("LVIII", 58)]
        [TestCase("MCMXCIV", 1994)]
        public void Solution2Tests(string s,
            int expectedResult)
        {
            int result = new Solution2().RomanToInt(s);

            result.Should().Be(expectedResult);
        }

        class Solution1
        {
            public int RomanToInt(string s)
            {
                int sum = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    // If there is enough string left to have a special case ( aka at least 2 ) 
                    if (i + 1 < s.Length)
                    {
                        var @case = s.Substring(i, 2);
                        if (_specialCases.ContainsKey(@case))
                        {
                            sum += _specialCases[@case];
                            i++;
                            continue;
                        }
                    }

                    sum += _romanNumerals[s[i]];
                }

                return sum;
            }

            private readonly Dictionary<string, int> _specialCases = new Dictionary<string, int>()
            {
                { "IV", 4 },
                { "IX", 9 },
                { "XL", 40 },
                { "XC", 90 },
                { "CD", 400 },
                { "CM", 900 },
            };

            private readonly Dictionary<char, int> _romanNumerals = new Dictionary<char, int>()
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 },
            };
        }

        public class Solution2
        {
            private static Dictionary<char, int> _map = new Dictionary<char, int>
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };

            // From
            // https://leetcode.com/problems/roman-to-integer/discuss/361735/C-Runtime%3A-84-ms-faster-than-94.84-Memory-Usage%3A-23.8-MB-less-than-8.70
            public int RomanToInt(string input)
            {
                if (input.Length <= 0)
                {
                    throw new Exception();
                }

                var total = 0;
                var last = 0;

                foreach (var rn in input)
                {
                    var current = TranslateToNumber(rn);

                    if (current > last)
                    {
                        total -= last * 2;
                    }

                    total += current;
                    last = current;
                }

                return total;
            }

            private static int TranslateToNumber(char c)
            {
                return _map[c];
            }
        }
    }
}