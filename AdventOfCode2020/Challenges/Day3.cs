using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day3 : Day
    {
        private readonly string[] _inputs;

        public Day3()
        {
            _inputs = ReadFile("Day3.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var treesEncountered = CalculateTreesInPath();
            var treesOnCombinedSlopes = CalculateTreesInCombinedSlopes();

            Console.WriteLine($"The sum of all trees encountered is:{treesEncountered}");
            Console.WriteLine($"The product of all trees in slopes is:{treesOnCombinedSlopes}");
        }

        private int CalculateTreesInPath()
        {
            const int horizontalSpeed = 3;
            const int verticalSpeed = 1;

            return CalculateTreesInPath(horizontalSpeed, verticalSpeed);
        }

        private long CalculateTreesInCombinedSlopes()
        {
            var firstSlope = new[]{1,1};
            var secondSlope = new[]{3,1};
            var thirdSlope = new[]{5,1};
            var fourthSlope = new[]{7,1};
            var fifthSlope = new[]{1,2};

            var slopeSpeeds = new List<int[]> {firstSlope,secondSlope,thirdSlope,fourthSlope,fifthSlope};
            long productOfAllTreesEncountered = 1;

            foreach (var speedPair in slopeSpeeds)
            {
                var treesInSlope = CalculateTreesInPath(speedPair[0], speedPair[1]);
                productOfAllTreesEncountered *= treesInSlope;
            }

            return productOfAllTreesEncountered;
        }

        private int CalculateTreesInPath(int horizontalSpeed, int verticalSpeed)
        {
            var formattedMap = _inputs.Select(row => row.ToCharArray()).ToArray();

            var treesEncountered = 0;
            var xPosition = 0;
            var yPosition = 0;

            do
            {
                xPosition += horizontalSpeed;
                yPosition += verticalSpeed;

                if (xPosition >= formattedMap[yPosition].Length)
                    xPosition -= formattedMap[yPosition].Length;

                if (formattedMap[yPosition][xPosition].Equals('#'))
                    treesEncountered++;

            } while (yPosition < formattedMap.Length - 1);

            return treesEncountered;
        }
    }
}