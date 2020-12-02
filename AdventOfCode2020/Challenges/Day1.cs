﻿using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Challenges
{
    public class Day1 : Day
    {
        private readonly int[] _inputs;

        public Day1()
        {
            _inputs = ReadFileToArray("Day1.txt");
        }

        public override void Start()
        {
            var firstResult = ExecuteFirstPart(_inputs);
            var secondResult = ExecuteSecondPart(_inputs);

            Console.WriteLine(firstResult);
            Console.WriteLine(secondResult);
        }

        private static int ExecuteSecondPart(IReadOnlyList<int> inputs)
        {
            var numbersFound = false;
            var firstNumber = 0;
            var secondNumber = 0;
            var thirdNumber = 0;

            for (var index = 0; index < inputs.Count - 2; index++)
            {
                firstNumber = inputs[index];
                for (var secondIndex = index + 1; secondIndex < inputs.Count - 1; secondIndex++)
                {
                    secondNumber = inputs[secondIndex];
                    for (var remainingIndex = secondIndex + 1; remainingIndex < inputs.Count; remainingIndex++)
                    {
                        thirdNumber = inputs[remainingIndex];
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

        private static int ExecuteFirstPart(IReadOnlyList<int> inputs)
        {
            var numbersFound = false;
            var firstNumber = 0;
            var secondNumber = 0;

            for (var index = 0; index < inputs.Count; index++)
            {
                firstNumber = inputs[index];
                for (var remainingIndex = index + 1; remainingIndex < inputs.Count; remainingIndex++)
                {
                    secondNumber = inputs[remainingIndex];
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
