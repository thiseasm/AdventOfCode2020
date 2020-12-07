using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day7 : Day
    {
        private readonly string[] _inputs;
        private readonly string _bagColor;
        private readonly List<string> _checkedColors;

        public Day7()
        {
            _inputs = ReadFile("Day7.txt");
            _bagColor = "shiny gold";
            _checkedColors = new List<string>();
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var bagsThatCanContainShinyGold = ProcessLuggageRules();

            Console.WriteLine($"The number of bags that can contain a Shiny Gold bag is: {bagsThatCanContainShinyGold}");
        }

        private int ProcessLuggageRules()
        {
            return GetEligibleContainers(_bagColor);
        }

        private int GetEligibleContainers(string bagColor)
        {
            var eligibleContainers = _inputs.Where(i => i.Contains(bagColor) && !i.StartsWith(bagColor)).ToList();
            var eligibleContainerCount = 0;

            foreach (var containerInfo in eligibleContainers)
            {
                var indexOfBag = containerInfo.IndexOf(" bags", StringComparison.Ordinal);
                var containerColor = containerInfo.Substring(0, indexOfBag);

                if(_checkedColors.Contains(containerColor))
                    continue;

                eligibleContainerCount += GetEligibleContainers(containerColor) + 1;
                _checkedColors.Add(containerColor);
            }

            return eligibleContainerCount;
        }
    }
}
