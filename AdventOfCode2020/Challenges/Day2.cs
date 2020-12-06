using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day2 : Day
    {
        private readonly string[] _inputs;

        public Day2()
        {
            _inputs = ReadFile("Day2.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var validPasswordsCount = ExecuteFirstPart();
            var validPasswordsUsingSecondRule = ExecuteSecondPart();

            Console.WriteLine($"Valid Passwords: {validPasswordsCount}");
            Console.WriteLine($"Valid Passwords using Second Rule: {validPasswordsUsingSecondRule}");
        }

        private int ExecuteSecondPart()
        {
            var validPasswordsFound = 0;
            foreach (var compositePassword in _inputs)
            {
                var ruleAndPassword = compositePassword.Split(": ");

                var password = ruleAndPassword[1];
                var positions = ruleAndPassword[0].Split(' ');
                var firstPosition = int.Parse(positions[0].Split('-')[0]);
                var secondPosition = int.Parse(positions[0].Split('-')[1]);
                var targetLetter = char.Parse(positions[1]);

                var firstPositionContainsLetter = password[firstPosition - 1].Equals(targetLetter);
                var secondPositionContainsLetter = password[secondPosition - 1].Equals(targetLetter);

                if (firstPositionContainsLetter ^ secondPositionContainsLetter)
                {
                    validPasswordsFound++;
                }
            }

            return validPasswordsFound;
        }

        private int ExecuteFirstPart()
        {
            var validPasswordsFound = 0;
            foreach (var compositePassword in _inputs)
            {
                var ruleAndPassword = compositePassword.Split(": ");

                var password = ruleAndPassword[1];
                var limits = ruleAndPassword[0].Split(' ');
                var lowerLimit = int.Parse(limits[0].Split('-')[0]);
                var upperLimit = int.Parse(limits[0].Split('-')[1]);
                var letterRequired = char.Parse(limits[1]);

                var timesFound = password.Count(letter => letter.Equals(letterRequired));
                if (timesFound >= lowerLimit && timesFound <= upperLimit)
                    validPasswordsFound++;
            }

            return validPasswordsFound;
        }

    }
}
