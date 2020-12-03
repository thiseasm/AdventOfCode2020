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

            // ReSharper disable once LocalizableElement
            Console.ReadKey();
        }
        private static Day CalculateDay()
        {
            const int date = 3;
            return date switch
            {
                1 => (Day) new Day1(),
                2 => (Day) new Day2(),
                3 => (Day) new Day3(),
                _ => new Day1()
            };
        }
    }
}