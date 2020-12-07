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
            var bagsThatCanContainShinyGold = GetEligibleContainers(_bagColor);
            var bagsThatNeedToBeContainedInShinyGold = GetRequiredContainedBagCount(_bagColor);

            Console.WriteLine($"The number of bags that can contain a Shiny Gold bag is: {bagsThatCanContainShinyGold}");
            Console.WriteLine($"The number of bags that need to be contained in a Shiny Gold bag is: {bagsThatNeedToBeContainedInShinyGold}");
        }

        private int GetRequiredContainedBagCount(string bagColor)
        {
            var containerInfo = _inputs.First(i => i.StartsWith(bagColor));
            var eligibleContainerCount = 0;

            if (containerInfo == null || containerInfo.Contains("no other bags"))
            {
                return eligibleContainerCount;
            }

            var indexOfContainedBags = containerInfo.IndexOf("contain", StringComparison.Ordinal) + 7;
            var containerQuantities = containerInfo.Substring(indexOfContainedBags)
                                                    .Replace('.', ' ').Replace("bags",string.Empty)
                                                    .Split(',').Select(i => i.Trim()).ToList();

            var containerInfoExtended = containerQuantities.ToDictionary(q => q.Substring(1).Trim(), q => int.Parse(q[0].ToString()));

            foreach (var color in containerInfoExtended.Keys)
            {
                eligibleContainerCount += containerInfoExtended[color]*(GetRequiredContainedBagCount(color) + 1);
            }

            return eligibleContainerCount;
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
