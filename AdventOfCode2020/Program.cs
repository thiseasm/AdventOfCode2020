using System;
using AdventOfCode2020.Challenges;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveChristmas();
        }

        private static void SaveChristmas()
        {
            var day = CalculateDay();
            day.Start();

            Console.ReadKey();
        }
        private static Day CalculateDay()
        {
            const int date = 11;
            return date switch
            {
                1 => (Day) new Day1(),
                2 => (Day) new Day2(),
                3 => (Day) new Day3(),
                4 => (Day) new Day4(),
                5 => (Day) new Day5(),
                6 => (Day) new Day6(),
                7 => (Day) new Day7(),
                8 => (Day) new Day8(),
                9 => (Day) new Day9(),
                10 => (Day) new Day10(),
                11 => (Day) new Day11(),
                _ => new Day1()
            };
        }
    }
}