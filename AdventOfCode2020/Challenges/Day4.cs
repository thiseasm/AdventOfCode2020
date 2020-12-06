using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Challenges
{
    public class Day4 : Day
    {
        private const int FieldLength = 4;

        private readonly string[] _inputs;
        private readonly string[] _requiredFields;
        

        public Day4()
        {
            _inputs = ReadFile("Day4.txt");
            _requiredFields = new[] {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var validPassports = CheckPassportsValidity(CheckPassportValidity);
            var validPassportsWithComplexMethod = CheckPassportsValidity(CheckAdvancedValidity);

            Console.WriteLine($"The number of passwords containing all fields present is: {validPassports}");
            Console.WriteLine($"The number of valid passwords present is: {validPassportsWithComplexMethod}");
        }

        private int CheckPassportsValidity(Func<string,bool> passportValidator)
        {
            var validPassportCount = 0;
            var passportToCheck = string.Empty;

            foreach (var line in _inputs)
            {
                if (line.Equals(string.Empty))
                {
                    var isValid = passportValidator(passportToCheck);
                    if (isValid) 
                        validPassportCount++;

                    passportToCheck = string.Empty;
                }
                else
                {
                    passportToCheck += string.Concat(" ", line);
                }
            }

            if (!passportToCheck.Equals(string.Empty) && passportValidator(passportToCheck))
                validPassportCount++;

            return validPassportCount;
        }

        private bool CheckAdvancedValidity(string passportToCheck)
        {
            var isValid = false;
            if (!CheckPassportValidity(passportToCheck))
            {
                return false;
            }

            foreach (var field in _requiredFields)
            {
                isValid = field switch
                {
                    "byr" => IsValidYear(passportToCheck, "byr", 1920, 2002),
                    "iyr" => IsValidYear(passportToCheck, "iyr", 2010, 2020),
                    "eyr" => IsValidYear(passportToCheck, "eyr", 2020, 2030),
                    "hgt" => IsValidHeight(passportToCheck),
                    "hcl" => IsValidHexColor(passportToCheck),
                    "ecl" => IsValidEyeColor(passportToCheck),
                    "pid" => IsValidPassportId(passportToCheck),
                    _ => isValid
                };

                if (!isValid)
                {
                    return false;
                }
            }

            return isValid;
        }

        private static bool IsValidPassportId(string passportToCheck)
        {
            var indexOfId = passportToCheck.IndexOf("pid", StringComparison.Ordinal) + FieldLength;
            var idSubstring = passportToCheck.Substring(indexOfId);
            var endOfProperty = idSubstring.IndexOf(' ');
            var passportId = endOfProperty > 0 ? passportToCheck.Substring(indexOfId, endOfProperty) : idSubstring;
            var nineDigitNumberRegex = new Regex(@"^\d{9}$");
            return nineDigitNumberRegex.IsMatch(passportId);
        }

        private static bool IsValidEyeColor(string passportToCheck)
        {
            var validColors = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            var indexOfEyeColor = passportToCheck.IndexOf("ecl", StringComparison.Ordinal) + FieldLength;
            var stringLength = indexOfEyeColor + 3 > passportToCheck.Length ? passportToCheck.Length - indexOfEyeColor : 3 ;
            var eyeColor = passportToCheck.Substring(indexOfEyeColor, stringLength);
            return validColors.Contains(eyeColor);
        }

        private static bool IsValidHexColor(string passportToCheck)
        {
            var indexOfHairColor = passportToCheck.IndexOf("hcl", StringComparison.Ordinal) + FieldLength;
            var stringLength = indexOfHairColor + 7 > passportToCheck.Length ? passportToCheck.Length - indexOfHairColor : 7 ;
            var hairColor = passportToCheck.Substring(indexOfHairColor, stringLength);
            var hexColorRegex = new Regex("^#([a-f0-9]{6})$");
            return hexColorRegex.IsMatch(hairColor);
        }

        private static bool IsValidHeight(string passportToCheck)
        {
            var indexOfHeight = passportToCheck.IndexOf("hgt", StringComparison.Ordinal) + FieldLength;
            var stringLength = indexOfHeight + 5 > passportToCheck.Length ? passportToCheck.Length - indexOfHeight : 5 ;
            var height = passportToCheck.Substring(indexOfHeight, stringLength);

            if (height.ToLower().Contains("cm"))
            {
                return IsValidHeightInCm(height);
            }

            if (height.ToLower().Contains("in"))
            {
                return IsValidHeightInIn(height);
            }

            return false;
        }

        private static bool IsValidHeightInIn(string height)
        {
            var heightInIn = height.Split("in")[0];
            var canParseValue = int.TryParse(heightInIn, out var heightNumber);
            return canParseValue && heightNumber >= 59 && heightNumber <= 76;
        }

        private static bool IsValidHeightInCm(string height)
        {
            var heightInCm = height.Split("cm")[0];
            var canParseValue = int.TryParse(heightInCm, out var heightNumber);
            return canParseValue && heightNumber >= 150 && heightNumber <= 193;
        }

        private static bool IsValidYear(string passportToCheck,string field, int lowerLimit, int upperLimit)
        {
            var index = passportToCheck.IndexOf(field, StringComparison.Ordinal) + FieldLength;
            var value = passportToCheck.Substring(index, 4);
            var canParseValue = int.TryParse(value, out var year);
            var isValid = (canParseValue && year >= lowerLimit && year <= upperLimit);
            return isValid;
        }

        private bool CheckPassportValidity(string passportToCheck)
        {
            // ReSharper disable once ConvertClosureToMethodGroup
            return _requiredFields.All(field => passportToCheck.Contains(field));
        }
    }
}
