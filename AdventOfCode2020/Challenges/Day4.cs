using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day4 : Day
    {
        private readonly string[] _inputs;

        public Day4()
        {
            _inputs = ReadFile("Day4.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var validPassports = CheckPassportsValidity(_inputs);

            Console.WriteLine($"The number of valid passwords present is: {validPassports}");
        }

        private static int CheckPassportsValidity(IEnumerable<string> inputs)
        {
            var validPassportCount = 0;
            var passportToCheck = string.Empty;

            foreach (var line in inputs)
            {
                if (line.Equals(string.Empty))
                {
                    var isValid = CheckPassportValidity(passportToCheck);
                    if (isValid) 
                        validPassportCount++;

                    passportToCheck = string.Empty;
                }
                else
                {
                    passportToCheck += string.Concat(" ", line);
                }
            }

            if (!passportToCheck.Equals(string.Empty) && CheckPassportValidity(passportToCheck))
                validPassportCount++;

            return validPassportCount;
        }

        private static bool CheckPassportValidity(string passportToCheck)
        {
            var requiredFields = new []{"byr","iyr","eyr","hgt","hcl","ecl","pid"};
            foreach (var field in requiredFields)
            {
                if (!passportToCheck.Contains(field))
                    return false;
            }

            return true;
        }
    }
}
