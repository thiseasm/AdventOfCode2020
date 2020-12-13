using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day11 : Day
    {
        private readonly string[] _inputs;

        public Day11()
        {
            _inputs = ReadFile("Day11.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var occupiedSeats = ModelLayoutChanges(false);
            var occupiedSeatsForSecondPart = ModelLayoutChanges(true);
            
            Console.WriteLine($"The number of occupied seats after modeling is: {occupiedSeats}");
            Console.WriteLine($"The number of occupied seats after the second modeling is: {occupiedSeatsForSecondPart}");
        }

        private int ModelLayoutChanges(bool executeAdvancedLogic)
        {
            var seatingPlanBefore = GenerateSeatingPlanFromInputs();

            var occupiedSeats = 0;
            var changeOccurred = true;
            var seatTolerance = executeAdvancedLogic ? 5 : 4;
            while (changeOccurred)
            {
                var seatingPlanAfter = new char[seatingPlanBefore.Length][];
                DeepCopyArray(seatingPlanBefore, seatingPlanAfter);

                occupiedSeats = 0;
                changeOccurred = ModelSeatChanges(seatingPlanBefore, seatingPlanAfter,seatTolerance, ref occupiedSeats);

                DeepCopyArray(seatingPlanAfter,seatingPlanBefore);
            }

            return occupiedSeats;
        }

        private static bool ModelSeatChanges(IReadOnlyList<char[]> seatingPlanBefore, IReadOnlyList<char[]> seatingPlanAfter,int occupiedSeatTolerance, ref int occupiedSeats)
        {
            var changeOccurred = false;

            for (var horizontalIndex = 0; horizontalIndex < seatingPlanBefore.Count; horizontalIndex++)
            {
                for (var verticalIndex = 0; verticalIndex < seatingPlanBefore[horizontalIndex].Length; verticalIndex++)
                {
                    if (seatingPlanBefore[horizontalIndex][verticalIndex].Equals('.'))
                    {
                        continue;
                    }

                    var adjacentSeats = occupiedSeatTolerance == 4 
                        ? GetAdjacentSeats(seatingPlanBefore, horizontalIndex, verticalIndex) 
                        : GetAdjacentSeatsInRange(seatingPlanBefore,horizontalIndex,verticalIndex);

                    if (seatingPlanBefore[horizontalIndex][verticalIndex].Equals('L'))
                    {
                        if (adjacentSeats.All(s => !s.Equals('#')))
                        {
                            seatingPlanAfter[horizontalIndex][verticalIndex] = '#';
                            changeOccurred = true;
                        }
                    }
                    else
                    {
                        if (adjacentSeats.Count(s => s.Equals('#')) >= occupiedSeatTolerance)
                        {
                            seatingPlanAfter[horizontalIndex][verticalIndex] = 'L';
                            changeOccurred = true;
                        }
                        else
                        {
                            occupiedSeats++;
                        }
                    }
                }
            }

            return changeOccurred;
        }

        private char[][] GenerateSeatingPlanFromInputs()
        {
            var seatingPlanBefore = new char[_inputs.Length][];
            for (var index = 0; index < _inputs.Length; index++)
            {
                var targetArray = _inputs[index].ToCharArray();
                seatingPlanBefore[index] = new char[targetArray.Length];
                Array.Copy(targetArray, seatingPlanBefore[index], targetArray.Length);
            }

            return seatingPlanBefore;
        }

        private static void DeepCopyArray(IReadOnlyList<char[]> source, IList<char[]> destination)
        {
            for (var index = 0; index < source.Count; index++)
            {
                var targetArray = source[index];
                destination[index] = new char[targetArray.Length];
                Array.Copy(targetArray, destination[index], targetArray.Length);
            }
        }

        private static IEnumerable<char> GetAdjacentSeatsInRange(IReadOnlyList<char[]> seatingPlan, in int horizontalIndex, in int verticalIndex)
        {
            var adjacentSeats = new List<char>();

            var hasRowBefore = HasRowBefore(horizontalIndex);
            var hasRowAfter = HasRowAfter(seatingPlan, horizontalIndex);
            var hasColumnBefore = HasColumnBefore(verticalIndex);
            var hasColumnAfter = HasColumnAfter(seatingPlan[horizontalIndex], verticalIndex);
            

            if(hasRowBefore)
            {
                var horizontal = horizontalIndex;
                var seatFound = false;
                while(!seatFound)
                {
                    var seat = seatingPlan[horizontal - 1][verticalIndex];
                    if (seat.Equals('.'))
                    {
                        horizontal -= 1;
                        if (!HasRowBefore(horizontal))
                            break;
                    }
                    else
                    {
                        adjacentSeats.Add(seat);
                        seatFound = true;
                    }

                }
            }

            if (hasColumnBefore)
            {
                var vertical = verticalIndex;
                var seatFound = false;
                while(!seatFound)
                {
                    var seat = seatingPlan[horizontalIndex][vertical - 1];
                    if (seat.Equals('.'))
                    {
                        vertical -= 1;
                        if (!HasColumnBefore(vertical))
                            break;
                    }
                    else
                    {
                        adjacentSeats.Add(seat);
                        seatFound = true;
                    }

                }
            }

            if(hasRowAfter)
            {
                var horizontal = horizontalIndex;
                var seatFound = false;
                while(!seatFound)
                {
                    var seat = seatingPlan[horizontal + 1][verticalIndex];
                    if (seat.Equals('.'))
                    {
                        horizontal += 1;
                        if (!HasRowAfter(seatingPlan,horizontal))
                            break;
                    }
                    else
                    {
                        adjacentSeats.Add(seat);
                        seatFound = true;
                    }

                }
            }

            if(hasColumnAfter)
            {
                var vertical = verticalIndex;
                var seatFound = false;
                while(!seatFound)
                {
                    var seat = seatingPlan[horizontalIndex][vertical + 1];
                    if (seat.Equals('.'))
                    {
                        vertical += 1;
                        if (!HasColumnAfter(seatingPlan[horizontalIndex],vertical))
                            break;
                    }
                    else
                    {
                        adjacentSeats.Add(seat);
                        seatFound = true;
                    }

                }
            }

            if(hasRowBefore && hasColumnBefore)
            {
                var horizontal = horizontalIndex;
                var vertical = verticalIndex;
                var seatFound = false;
                while(!seatFound)
                {
                    var seat = seatingPlan[horizontal - 1][vertical - 1];
                    if (seat.Equals('.'))
                    {
                        horizontal -= 1;
                        vertical -= 1;
                        if (!(HasRowBefore(horizontal) && HasColumnBefore(vertical)))
                            break;
                    }
                    else
                    {
                        adjacentSeats.Add(seat);
                        seatFound = true;
                    }

                }
            }

            if(hasRowAfter && hasColumnBefore)
            {
                var horizontal = horizontalIndex;
                var vertical = verticalIndex;
                var seatFound = false;
                while(!seatFound)
                {
                    var seat = seatingPlan[horizontal + 1][vertical - 1];
                    if (seat.Equals('.'))
                    {
                        horizontal += 1;
                        vertical -= 1;
                        if (!(HasRowAfter(seatingPlan,horizontal) && HasColumnBefore(vertical)))
                            break;
                    }
                    else
                    {
                        adjacentSeats.Add(seat);
                        seatFound = true;
                    }

                }
            }

            if(hasRowBefore && hasColumnAfter)
            {
                var horizontal = horizontalIndex;
                var vertical = verticalIndex;
                var seatFound = false;
                while(!seatFound)
                {
                    var seat = seatingPlan[horizontal - 1][vertical + 1];
                    if (seat.Equals('.'))
                    {
                        horizontal -= 1;
                        vertical += 1;
                        if (!(HasRowBefore(horizontal) && HasColumnAfter(seatingPlan[horizontal],vertical)))
                            break;
                    }
                    else
                    {
                        adjacentSeats.Add(seat);
                        seatFound = true;
                    }

                }
            }           

            if(hasRowAfter && hasColumnAfter)
            {
                var horizontal = horizontalIndex;
                var vertical = verticalIndex;
                var seatFound = false;
                while(!seatFound)
                {
                    var seat = seatingPlan[horizontal + 1][vertical + 1];
                    if (seat.Equals('.'))
                    {
                        horizontal += 1;
                        vertical += 1;
                        if (!(HasRowAfter(seatingPlan,horizontal) && HasColumnAfter(seatingPlan[horizontal],vertical)))
                            break;
                    }
                    else
                    {
                        adjacentSeats.Add(seat);
                        seatFound = true;
                    }

                }

            }

            return adjacentSeats;
        }

        private static IEnumerable<char> GetAdjacentSeats(IReadOnlyList<char[]> seatingPlan,int horizontalIndex, int verticalIndex)
        {
            var adjacentSeats = new List<char>();

            var hasRowBefore = HasRowBefore(horizontalIndex);
            var hasRowAfter = HasRowAfter(seatingPlan, horizontalIndex);
            var hasColumnBefore = HasColumnBefore(verticalIndex);
            var hasColumnAfter = HasColumnAfter(seatingPlan[horizontalIndex], verticalIndex);
            

            if(hasRowBefore)
                adjacentSeats.Add(seatingPlan[horizontalIndex - 1][verticalIndex]);
            if (hasColumnBefore)
                adjacentSeats.Add(seatingPlan[horizontalIndex][verticalIndex - 1]);
            if(hasRowAfter)
                adjacentSeats.Add(seatingPlan[horizontalIndex + 1][verticalIndex]);
            if(hasColumnAfter)
                adjacentSeats.Add(seatingPlan[horizontalIndex][verticalIndex + 1]);

            if(hasRowBefore && hasColumnBefore)
                adjacentSeats.Add(seatingPlan[horizontalIndex - 1][verticalIndex - 1]);
            if(hasRowAfter && hasColumnBefore)
                adjacentSeats.Add(seatingPlan[horizontalIndex + 1][verticalIndex - 1]);
            if(hasRowBefore && hasColumnAfter)
                adjacentSeats.Add(seatingPlan[horizontalIndex - 1][verticalIndex + 1]);
            if(hasRowAfter && hasColumnAfter)
                adjacentSeats.Add(seatingPlan[horizontalIndex + 1][verticalIndex + 1]);

            return adjacentSeats;
        }

        private static bool HasRowBefore(int horizontalIndex) =>  horizontalIndex > 0;
        private static bool HasRowAfter(IReadOnlyCollection<char[]> seatingPlan, int horizontalIndex) =>  horizontalIndex < seatingPlan.Count - 1;
        private static bool HasColumnBefore(int verticalIndex) =>  verticalIndex > 0;
        private static bool HasColumnAfter(IReadOnlyCollection<char> rowPlan, int verticalIndex) =>  verticalIndex < rowPlan.Count - 1;
    }
}
