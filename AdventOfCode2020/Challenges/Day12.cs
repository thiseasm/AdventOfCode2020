using System;
using System.Diagnostics.CodeAnalysis;
using AdventOfCode2020.Helpers.Day12;

namespace AdventOfCode2020.Challenges
{
    public class Day12 : Day
    {
        private readonly string[] _inputs;

        public Day12()
        {
            _inputs = ReadFile("Day12.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var currentManhattanDistance = CalculateManhattanDistance(new PositionHelper());
            var currentManhattanDistanceUsingWaypoint = CalculateManhattanDistance(new WaypointHelper());

            Console.WriteLine($"The ship's Manhattan Distance is: {currentManhattanDistance}");
            Console.WriteLine($"The ship's Manhattan Distance using Waypoint is: {currentManhattanDistanceUsingWaypoint}");
        }

        private int CalculateManhattanDistance(PositionHelper helper)
        {
            foreach (var action in _inputs)
            {
                helper.ExecuteAction(action);
            }

            return helper.GetCurrentPosition();
        }
    }
}
