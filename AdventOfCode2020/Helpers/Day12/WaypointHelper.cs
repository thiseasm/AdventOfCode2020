using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Helpers.Day12
{
    public class WaypointHelper : PositionHelper
    {
        public Dictionary<Direction,int> ShipPosition { get; }

        public WaypointHelper()
        {
            ShipPosition = new Dictionary<Direction, int>
            {
                {Direction.North, 0},
                {Direction.East, 0},
                {Direction.South, 0},
                {Direction.West, 0}
            };

            Location[Direction.East] = 10;
            Location[Direction.North] = 1;
        }

        public override void ExecuteAction(string action)
        {
            var (command, value) = ParseAction(action);

            switch (command)
            {
                case 'F':
                    MoveToWaypoint(value);
                    break;
                case 'L':
                case 'R':
                    ChangeFacingDirection(command,value);
                    break;
                default:
                    MoveToDirection(command,value);
                    break;
            }
        }

        private void MoveToWaypoint(int timesToWaypoint)
        {
            foreach (var direction in Location.Keys)
            {
                var command = direction switch
                {
                    Direction.East => 'E',
                    Direction.North => 'N',
                    Direction.South => 'S',
                    Direction.West => 'W',
                    _ => throw new ArgumentOutOfRangeException()
                };

                var units = Location[direction] * timesToWaypoint;

                switch (command)
                {
                    case 'N':
                        MoveShip(units,Direction.North,Direction.South);
                        break;
                    case 'S':
                        MoveShip(units,Direction.South,Direction.North);
                        break;
                    case 'E':
                        MoveShip(units,Direction.East,Direction.West);
                        break;
                    case 'W':
                        MoveShip(units,Direction.West,Direction.East);
                        break;
                }
            }
            
        }
        public override int GetCurrentPosition() =>  ShipPosition.Values.Sum();

        protected void MoveShip(int units, Direction toDirection, Direction opposite)
        {
            if (ShipPosition[opposite] > 0)
            {
                ShipPosition[opposite] -= units;
                if (ShipPosition[opposite] < 0)
                {
                    ShipPosition[toDirection] += -ShipPosition[opposite];
                    ShipPosition[opposite] = 0;
                }
            }
            else
            {
                ShipPosition[toDirection] += units;
            }
        }

        protected override void ChangeFacingDirection(char direction, int degrees)
        {
            if (direction.Equals('R'))
            {
                while (degrees > 0)
                {
                    var keys = Location.Keys.ToArray();
                    var values = Location.Values.ToArray();

                    Location[keys[0]] = values[3];
                    Location[keys[1]] = values[0];
                    Location[keys[2]] = values[1];
                    Location[keys[3]] = values[2];
                    
                    degrees -= 90;
                }
            }
            else
            {
                while (degrees > 0)
                {
                    var keys = Location.Keys.ToArray();
                    var values = Location.Values.ToArray();

                    Location[keys[0]] = values[1];
                    Location[keys[1]] = values[2];
                    Location[keys[2]] = values[3];
                    Location[keys[3]] = values[0];

                    degrees -= 90;
                }
            }
        }
    }
}
