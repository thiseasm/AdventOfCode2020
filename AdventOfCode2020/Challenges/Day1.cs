using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2020.Challenges
{
    public class Day1 : Day
    {
        private readonly int[] _inputs;

        public Day1()
        {
            _inputs = ReadIntegerFile("Day1.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var firstProduct = ExecuteFirstPart();
            var secondProduct = ExecuteSecondPart();

            Console.WriteLine($"The product of the two numbers is: {firstProduct}");
            Console.WriteLine($"The product of the three numbers is: {secondProduct}");
        }

        private int ExecuteSecondPart()
        {
            var numbersFound = false;
            var firstNumber = 0;
            var secondNumber = 0;
            var thirdNumber = 0;

            for (var index = 0; index < _inputs.Length - 2; index++)
            {
                firstNumber = _inputs[index];
                for (var secondIndex = index + 1; secondIndex < _inputs.Length - 1; secondIndex++)
                {
                    secondNumber = _inputs[secondIndex];
                    for (var remainingIndex = secondIndex + 1; remainingIndex < _inputs.Length; remainingIndex++)
                    {
                        thirdNumber = _inputs[remainingIndex];
                        if (firstNumber + secondNumber + thirdNumber != 2020) continue;

                        numbersFound = true;
                        break;
                    }

                    if (numbersFound) break;
                }

                if (numbersFound) break;
            }
            

            return firstNumber * secondNumber * thirdNumber;
        }

        private int ExecuteFirstPart()
        {
            var numbersFound = false;
            var firstNumber = 0;
            var secondNumber = 0;

            for (var index = 0; index < _inputs.Length; index++)
            {
                firstNumber = _inputs[index];
                for (var remainingIndex = index + 1; remainingIndex < _inputs.Length; remainingIndex++)
                {
                    secondNumber = _inputs[remainingIndex];
                    if (firstNumber + secondNumber != 2020) continue;

                    numbersFound = true;
                    break;
                }

                if (numbersFound) break;
            }

            return firstNumber * secondNumber;

        }
    }
}
