using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class LongestPasswordTests
    {
        [TestCase("test 5 a0A pass007 ?xy1", 7)]
        [TestCase("zaq!2#edc", -1)]
        public void Tests(string S, int expectedResult)
        {
            var result = LongestPassword(S);

            result.Should().Be(expectedResult);
        }

        public int LongestPassword(string S)
        {
            string[] words = S.Split(' ');
            var validPasswords = GetValidPasswords(words).ToList();
            if (!validPasswords.Any())
            {
                return -1;
            }

            return validPasswords.Max(m => m.Length);
        }

        private IEnumerable<string> GetValidPasswords(string[] words)
        {
            foreach (string word in words)
            {
                if (IsValidPassword(word))
                {
                    yield return word;
                }
            }
        }

        private bool IsValidPassword(string word)
        {
            const string alphaNumericPattern = @"^[a-zA-Z0-9]*$";
            const string alphaPattern = @"[a-zA-Z]";
            const string numericPattern = @"[0-9]";

            if (!new Regex(alphaNumericPattern).Match(word).Success)
            {
                return false;
            }

            if (new Regex(alphaPattern).Matches(word).Count % 2 == 1)
            {
                return false;
            }

            if (new Regex(numericPattern).Matches(word).Count % 2 == 0)
            {
                return false;
            }

            return true;
        }
    }
}