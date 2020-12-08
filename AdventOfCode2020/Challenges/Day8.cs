using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2020.Challenges
{
    public class Day8 : Day
    {
        private readonly string[] _inputs;

        public Day8()
        {
            _inputs = ReadFile("Day8.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var accumulatorValue = RunProgramOnce();

            Console.WriteLine($"{accumulatorValue}");
        }

        private int RunProgramOnce()
        {
            var accumulatorValue = 0;
            var executedCommands = new List<int>();
            var index = 0;

            do
            {
                var command = _inputs[index].Split(' ');
                executedCommands.Add(index);
                switch (command[0])
                {
                    case "acc":
                        var value = int.Parse(command[1]);
                        accumulatorValue += value;
                        index++;
                        break;
                    case "jmp":
                        var step = int.Parse(command[1]);
                        index += step;
                        break;
                    case "nop":
                        index++;
                        break;
                }
            } while (!executedCommands.Contains(index));

            return accumulatorValue;
        }
    }
}
