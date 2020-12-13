using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day13 : Day
    {
        private readonly string[] _inputs;

        public Day13()
        {
            _inputs = ReadFile("Day13.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var minutesAndIdProduct = CalculateProduct();

            Console.WriteLine($"The product of waiting time and bus ID is: {minutesAndIdProduct}");
        }

        private int CalculateProduct()
        {
            var waitingTime = 100000; // Something extremely big to ensure that it will be replaced
            var chosenBusId = 0;

            var earliestTimestamp = int.Parse(_inputs[0]);
            var busIds = _inputs[1].Split(',').Where(id => !id.Equals("x")).Select(int.Parse).ToList();

            foreach (var id in busIds)
            {
                var nextBusArrival = 0;

                while (nextBusArrival < earliestTimestamp)
                {
                    nextBusArrival += id;
                }

                var expectedWaitingTime = nextBusArrival - earliestTimestamp;

                if (expectedWaitingTime < waitingTime)
                {
                    waitingTime = expectedWaitingTime;
                    chosenBusId = id;
                }
            }

            return waitingTime * chosenBusId;
        }
    }
}
