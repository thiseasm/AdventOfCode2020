using System;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day9 : Day
    {
        public readonly long[] _inputs;

        public Day9()
        {
            _inputs = ReadLongFile("Day9.txt");
        }
        public override void Start()
        {
            var numberThatDefiesPreamble = CheckXmasPreambles();

            Console.WriteLine(numberThatDefiesPreamble);
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
