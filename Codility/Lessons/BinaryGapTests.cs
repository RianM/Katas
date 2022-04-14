using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class BinaryGapTests
    {
        [TestCase(9,2)]
        [TestCase(529,4)]
        [TestCase(20,1)]
        [TestCase(15,0)]
        [TestCase(32,0)]
        [TestCase(1041,5)]
        public void Tests(int N, int expectedResult)
        {
            var result = BinaryGap(N);
            
            Assert.True(result==expectedResult,$"result is {result}, expected {expectedResult}");
        }
        
        public int BinaryGap(int N)
        {
            string binary = Convert.ToString(N, 2);

            bool inGap = false;
            int max = 0;
            int current = 0;
            foreach (char c in binary)
            {
                if (c == '1')
                {
                    if (inGap)
                    {
                        if (current > max)
                        {
                            max = current;
                        }
                        current = 0;
                    }
                    else
                    {
                        inGap = true;
                    }
                }
                else if(inGap)
                {
                    current++;
                }
            }

            return max;
        }
    }
}