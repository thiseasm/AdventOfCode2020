using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day9 : Day
    {
        private readonly long[] _inputs;

        public Day9()
        {
            _inputs = ReadLongFile("Day9.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var numberThatDefiesPreamble = CheckXmasPreambles();
            var sumOfUpperAndLowerLimits = GetSumOfLimits(numberThatDefiesPreamble);

            Console.WriteLine($"The number that defies the preamble is: {numberThatDefiesPreamble}");
            Console.WriteLine($"The sum of the lowest and highest number is : {sumOfUpperAndLowerLimits}");
        }

        private long GetSumOfLimits(long numberThatDefiesPreamble)
        {
            long sumOfLimits = 0;
            var groupFound = false;

            for (var counter = 0; _inputs[counter] < numberThatDefiesPreamble; counter++)
            {
                var targetNumbers = new long[25];
                Array.Copy(_inputs,counter,targetNumbers,0,25);

                for (var size = 2; size < 25; size++)
                {
                    for (var index = 0; index < 25 - size; index++)
                    {
                        var groupToCheck = new long[size];
                        Array.Copy(targetNumbers,index,groupToCheck,0,size);

                        if (groupToCheck.Sum() != numberThatDefiesPreamble)
                            continue;

                        Array.Sort(groupToCheck);
                        sumOfLimits = groupToCheck[0] + groupToCheck[^1];
                        groupFound = true;
                        break;
                    }

                    if (groupFound)
                        break;
                }

                if (groupFound)
                    break;
            }

            return sumOfLimits;
        }

        private long CheckXmasPreambles()
        {
            long valueThatDefiesProtocol = 0;
            for (var counter = 25; counter < _inputs.Length; counter++)
            {
                var combinationFound = false;
                var preamble = _inputs.ToList().GetRange(counter - 25, 26);
                for (var preambleIndex = 0; preambleIndex < preamble.Count - 1; preambleIndex++)
                {
                    for (var remainingIndex = 0; remainingIndex < preamble.Count - 1; remainingIndex++)
                    {
                        if (preamble[preambleIndex] + preamble[remainingIndex] != preamble[25])
                            continue;

                        combinationFound = true;
                        break;
                    }

                    if (combinationFound)
                        break;
                }

                if(combinationFound)
                    continue;

                valueThatDefiesProtocol = _inputs[counter];
                break;
            }

            return valueThatDefiesProtocol;
        }

    }
}
