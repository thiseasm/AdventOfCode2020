using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public  class Day14 : Day
    {
        private readonly string[] _inputs;

        public Day14()
        {
            _inputs = ReadFile("Day14.txt");
        }

        public override void Start()
        {
            var sumOfValues = CalculateValuesInMemory();

            Console.WriteLine($"The sum of values in memory is: {sumOfValues}");
        }

        private long CalculateValuesInMemory()
        {
            var mask = new char[36];
            var memory = new Dictionary<int,long>();

            foreach (var line in _inputs)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split('=')[1].Trim().ToCharArray();
                }
                else
                {
                    var value = int.Parse(line.Split('=')[1].Trim());
                    var valueAsBinary = ConvertTo36Bit(value, mask);

                    for (var index = 0; index < mask.Length; index++)
                    {
                        if (mask[index].Equals('X'))
                        {
                            continue;
                        }

                        valueAsBinary[index] = mask[index];
                    }

                    var maskedValue = ConvertToInt(valueAsBinary);
                    var memoryAddress = int.Parse(line.Split('[', ']')[1]);
                    if (!memory.ContainsKey(memoryAddress))
                    {
                        memory.Add(memoryAddress,0);
                    }
                    memory[memoryAddress] = maskedValue;
                }
            }

            return memory.Values.Sum();
        }

        private static List<char> ConvertTo36Bit(int value, IReadOnlyCollection<char> mask)
        {
            var valueAsBinary = new List<char>();

            while (value != 0)
            {
                valueAsBinary.Add(char.Parse((value % 2).ToString()));
                value /= 2;
            }

            while (mask.Count > valueAsBinary.Count)
            {
                valueAsBinary.Add('0');
            }

            valueAsBinary.Reverse();
            return valueAsBinary;
        }

        private static long ConvertToInt(List<char> valueAsBinary)
        {
            var result = 0d;
            valueAsBinary.Reverse();

            for (var index = 0; index < valueAsBinary.Count; index++)
            {
                if (valueAsBinary[index].Equals('1'))
                    result += Math.Pow(2,index);
            }

            return (long) result;
        }
    }
}
