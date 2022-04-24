using System;
using System.Linq;

namespace TestHelpers
{
    public class TestHelpers
    {
        private static readonly Random Random = new Random();
        
        // Stolen from https://www.codegrepper.com/code-examples/csharp/c%23+random+alphanumeric+string
        public static string CreateString(string chars, int length)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}