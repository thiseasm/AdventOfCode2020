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
            var date = 2;
            return date switch
            {
                1 => (Day) new Day1(),
                2 => (Day) new Day2(),
                _ => new Day1()
            };
        }
    }
}