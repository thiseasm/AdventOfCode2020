using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day2 : Day
    {
        private string[] _inputs;

        public Day2()
        {
            _inputs = ReadFile("Day2.txt");
        }

        public override void Start()
        {
            var validPasswordsCount = ExecuteFirstPart(_inputs);

            Console.WriteLine($"Valid Passwords: {validPasswordsCount}");
        }

        private static int ExecuteFirstPart(IEnumerable<string> inputs)
        {
            var validPasswordsFound = 0;
            foreach (var compositePassword in inputs)
            {
                var ruleAndPassword = compositePassword.Split(':');

                var password = ruleAndPassword[1];
                var limits = ruleAndPassword[0].Split(' ');
                var lowerLimit = int.Parse(limits[0].Split('-')[0]);
                var upperLimit = int.Parse(limits[0].Split('-')[1]);
                var letterRequired = limits[1];

                var timesFound = password.Count(letter => letter.ToString().Equals(letterRequired));
                if (timesFound >= lowerLimit && timesFound <= upperLimit)
                    validPasswordsFound++;
            }

            return validPasswordsFound;
        }
    }
}
