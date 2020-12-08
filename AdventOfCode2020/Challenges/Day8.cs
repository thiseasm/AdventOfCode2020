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
            var fixedAccumulatorValue = RunAndRepairProgram();

            Console.WriteLine($"The accumulator value at the end of the first pass is: {accumulatorValue}");
            Console.WriteLine($"The accumulator value of the fixed OS is: {fixedAccumulatorValue}");
        }

        private int RunAndRepairProgram()
        {
            var accumulatorValue = 0;
            for (var counter = 0; counter < _inputs.Length; counter++)
            {
                var inputs = ReadFile("Day8.txt");
                switch (_inputs[counter].Split(' ')[0])
                {
                    case "jmp":
                        inputs[counter] = _inputs[counter].Replace("jmp", "nop");
                        break;
                    case "nop":
                        inputs[counter] = _inputs[counter].Replace("nop", "nop");
                        break;
                    default:
                        continue;
                }

                var completedSuccessfully = CanRunToCompletion(inputs, out accumulatorValue);

                if (completedSuccessfully)
                    break;
            }


            return accumulatorValue;
        }

        private static bool CanRunToCompletion(IReadOnlyList<string> inputs,out int accumulatorValue)
        {
            accumulatorValue = 0;
            var executedCommands = new List<int>();
            var index = 0;

            do
            {
                var command = inputs[index].Split(' ');
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

                if (index == inputs.Count)
                {
                    return true;
                }

            } while (!executedCommands.Contains(index));

            accumulatorValue = 0;
            return false;
        }

        private int RunProgramOnce()
        {
            var accumulatorValue = 0;
            var index = 0;
            var executedCommands = new List<int>();

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
