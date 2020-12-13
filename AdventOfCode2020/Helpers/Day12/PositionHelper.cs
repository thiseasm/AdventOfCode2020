using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Helpers.Day12
{
    public class PositionHelper
    {
        public Dictionary<Direction,int> Location { get; }
        public Direction FacingDirection { get; protected set; }

        public PositionHelper()
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

        public virtual void ExecuteAction(string action)
        {
            var (command, value) = ParseAction(action);

            switch (command)
            {
                case 'F':
                    MoveToDirection(FacingDirection.ToString()[0],value);
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

        public virtual int GetCurrentPosition() => Location.Values.Sum();

        protected virtual void ChangeFacingDirection(char direction, int degrees)
        {
            if (direction.Equals('R'))
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

        protected void MoveToDirection(char command, int units)
        {
            switch (command)
            {
                case 'N':
                    MoveUsingNavigation(units,Direction.North,Direction.South);
                    break;
                case 'S':
                    MoveUsingNavigation(units,Direction.South,Direction.North);
                    break;
                case 'E':
                    MoveUsingNavigation(units,Direction.East,Direction.West);
                    break;
                case 'W':
                    MoveUsingNavigation(units,Direction.West,Direction.East);
                    break;
            }
        }

        protected void MoveUsingNavigation(int units, Direction toDirection, Direction opposite)
        {
            if (Location[opposite] > 0)
            {
                Location[opposite] -= units;
                if (Location[opposite] < 0)
                {
                    Location[toDirection] += -Location[opposite];
                    Location[opposite] = 0;
                }
            }
            else
            {
                Location[toDirection] += units;
            }
        }

        protected static KeyValuePair<char,int> ParseAction(string action)
        {
            var command = action[0];
            var units = int.Parse(action.Substring(1));

            return new KeyValuePair<char, int>(command,units);
        }
    }

}