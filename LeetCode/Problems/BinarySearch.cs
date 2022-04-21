using System;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Problems
{
    [TestFixture]
    public class BinarySearch
    {
        [TestCase(new int[] { -1, 0, 3, 5, 9, 12 }, 9, 4)]
        [TestCase(new int[] { -1, 0, 3, 5, 9, 12 }, 2, -1)]
        public void Test(int[] nums,
            int target,
            int expectedResult)
        {
            var result = new Solution().Search(nums, target);
            result.Should().Be(expectedResult);
        }

        class Solution
        {
            public int Search(int[] nums,
                int target)
            {
                int l = 0;
                int r = nums.Length - 1;

                while (l <= r)
                {
                    var searchIndex = (r - l) / 2 + l;
                    
                    if (nums[searchIndex] == target)
                    {
                        return searchIndex;
                    }

                    if (nums[searchIndex] > target)
                    {
                        r = searchIndex - 1;
                    }
                    else
                    {
                        l = searchIndex + 1;
                    }
                }

                return -1;
            }
        }
    }
}