using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day12 : Day
    {
        private readonly string[] _inputs;

        public Day12()
        {
            _inputs = ReadFile("Day12.txt");
        }

        public override void Start()
        {
            var currentManhattanDistance = CalculateManhattanDistance();

            Console.WriteLine($"The ship's Manhattan Distance is: {currentManhattanDistance}");
        }

        private int CalculateManhattanDistance()
        {
            var currentPosition = new Position();
            foreach (var action in _inputs)
            {
                currentPosition.ExecuteAction(action);
            }

            return currentPosition.Location.Values.Sum();
        }
    }

    public class Position
    {
        public Dictionary<Direction,int> Location { get; private set; }
        public Direction FacingDirection { get; private set; }

        public Position()
        {
            Location= new Dictionary<Direction, int>
            {
                {Direction.North, 0},
                {Direction.East, 0},
                {Direction.South, 0},
                {Direction.West, 0}
            };

            FacingDirection = Direction.East;
        }

        public void ExecuteAction(string action)
        {
            var parsedAction = ParseAction(action);

            switch (parsedAction.Key)
            {
                case 'F':
                    var command = new KeyValuePair<char, int>(FacingDirection.ToString()[0], parsedAction.Value);
                    MoveToDirection(command);
                    break;
                case 'L':
                case 'R':
                    ChangeFacingDirection(parsedAction);
                    break;
                default:
                    MoveToDirection(parsedAction);
                    break;
            }
        }

        private void ChangeFacingDirection(KeyValuePair<char, int> parsedAction)
        {
            var degrees = parsedAction.Value;
            if (parsedAction.Key.Equals('R'))
            {
                while (degrees > 0)
                {
                    FacingDirection += 1;
                    if ((int)FacingDirection > 3)
                    {
                        FacingDirection = 0;
                    }

                    degrees -= 90;
                }
            }
            else
            {
                while (degrees > 0)
                {
                    FacingDirection -= 1;
                    if ((int)FacingDirection < 0)
                    {
                        FacingDirection = (Direction)3;
                    }

                    degrees -= 90;
                }
            }
        }

        private void MoveToDirection(KeyValuePair<char, int> parsedAction)
        {
            switch (parsedAction.Key)
            {
                case 'N':
                    MoveUsingNavigation(parsedAction,Direction.North,Direction.South);
                    break;
                case 'S':
                    MoveUsingNavigation(parsedAction,Direction.South,Direction.North);
                    break;
                case 'E':
                    MoveUsingNavigation(parsedAction,Direction.East,Direction.West);
                    break;
                case 'W':
                    MoveUsingNavigation(parsedAction,Direction.West,Direction.East);
                    break;
            }
        }

        private void MoveUsingNavigation(KeyValuePair<char, int> parsedAction, Direction toDirection, Direction opposite)
        {
            if (Location[opposite] > 0)
            {
                Location[opposite] -= parsedAction.Value;
                if (Location[opposite] < 0)
                {
                    Location[toDirection] += -Location[opposite];
                    Location[opposite] = 0;
                }
            }
            else
            {
                Location[toDirection] += parsedAction.Value;
            }
        }

        private static KeyValuePair<char,int> ParseAction(string action)
        {
            var command = action[0];
            var units = int.Parse(action.Substring(1));

            return new KeyValuePair<char, int>(command,units);
        }
    }

    public enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }
}
