using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode2020.Challenges
{
    public class Day10 : Day
    {
        private readonly int[] _inputs;

        public Day10()
        {
            _inputs = ReadIntegerFile("Day10.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var joltageProduct = CalculateJoltageProduct();
            var adapterCombinations = CalculateAvailableCombinations();

            Console.WriteLine($"The product of all adapters is: {joltageProduct}");
            Console.WriteLine($"The available combinations of adapters is: {adapterCombinations}");
        }

        private long CalculateAvailableCombinations()
        {
            var joltRatings = GetJoltRatingsIncludingEndpoints();
            var combinationsPerAdapter = new long[joltRatings.Length];
            combinationsPerAdapter[0] = 1;

            for (var index = 1; index < combinationsPerAdapter.Length; index++)
            {
                for (var targetIndex = 0; targetIndex < index; targetIndex++)
                {
                    if (joltRatings[index] - joltRatings[targetIndex] <= 3)
                    {
                        combinationsPerAdapter[index] += combinationsPerAdapter[targetIndex];
                    }
                }
            }

            return combinationsPerAdapter[^1];

        }

        private int CalculateJoltageProduct()
        {
            Array.Sort(_inputs);
            var ratings = GetJoltRatingsIncludingEndpoints();

            var oneJoltDifferences = 0;
            var threeJoltDifferences = 0;

            for (var counter = 1; counter < ratings.Length; counter++)
            {
                if (ratings[counter] - ratings[counter - 1] == 1)
                    oneJoltDifferences++;
                if (ratings[counter] - ratings[counter - 1] == 3)
                    threeJoltDifferences++;
            }

            return oneJoltDifferences * threeJoltDifferences;
        }

        private int[] GetJoltRatingsIncludingEndpoints()
        {
            var ratings = new int[_inputs.Length + 2];
            Array.Copy(_inputs, 0, ratings, 1, _inputs.Length);

            //Include device ratings
            ratings[^1] = ratings[^2] + 3;

            Array.Sort(ratings);
            return ratings;
        }
    }
}
