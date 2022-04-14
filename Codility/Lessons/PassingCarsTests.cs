using FluentAssertions;
using NUnit.Framework;

namespace Hacking
{
    [TestFixture]
    public class PassingCarsTests
    {
        [TestCase(new int[] { 0, 1, 0, 1,1 }, 5)]
        public void Tests(int[] A, int expectedResult)
        {
            var result = PassingCars(A);

            result.Should().Be(expectedResult);
        }

        // O(n)
        public int PassingCars(int[] A)
        {
            int[] carsGoingEast = new int[A.Length];
            int sum = 0;

            for (int i = 0; i < A.Length; i++)
            {
                // if the car is going east, add it to the sum of cars goings
                if (A[i] == 0)
                {
                    sum++;
                }
                
                carsGoingEast[i] = sum;
            }

            int passing = 0;

            for (int i = 0; i < A.Length; i++)
            {
                // if the car is going west, add the amount of cars going east to the passing sum
                if (A[i] == 1)
                {
                    passing += carsGoingEast[i];

                    if (passing > 1000000000)
                    {
                        return -1;
                    }
                }
            }

            return passing;
        }

        // O(n X n)
        public int PassingCars1(int[] A)
        {
            int passing = 0;
            for (int i = 0; i < A.Length-1; i++)
            {
                if (A[i] == 0)
                {
                    for (int j = i + 1; j < A.Length; j++)
                    {
                        if (A[j] == 1)
                        {
                            passing++;
                        }
                    }
                }
            }

            return passing;
        }
    }
}