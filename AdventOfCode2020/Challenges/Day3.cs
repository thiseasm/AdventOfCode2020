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
            var treesEncountered = CalculateTreesInPath(_inputs);

            Console.WriteLine($"The sum of all trees encountered is:{treesEncountered}");
        }

        private static int CalculateTreesInPath(IEnumerable<string> inputs)
        {
            var formattedMap = inputs.Select(row => row.ToCharArray()).ToArray();

            var treesEncountered = 0;
            var xPosition = 0;
            var yPosition = 0;

            do
            {
                xPosition += 3;
                yPosition += 1;

                if (xPosition >= formattedMap[yPosition].Length)
                    xPosition -= formattedMap[yPosition].Length;

                if (formattedMap[yPosition][xPosition].Equals('#'))
                    treesEncountered++;

            } while (yPosition < formattedMap.Length - 1);

            return treesEncountered;
        }
    }
}