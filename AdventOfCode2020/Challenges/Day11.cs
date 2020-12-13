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
            var occupiedSeats = ModelLayoutChanges();

            Console.WriteLine($"The number of occupied seats after modeling is: {occupiedSeats}");
        }

        private int ModelLayoutChanges()
        {
            var seatingPlanBefore = new char[_inputs.Length][];
            for (var index = 0; index < _inputs.Length; index++)
            {
                var targetArray = _inputs[index].ToCharArray();
                seatingPlanBefore[index] = new char[targetArray.Length];
                Array.Copy(targetArray,seatingPlanBefore[index],targetArray.Length);
            }

            var occupiedSeats = 0;
            var changeOccurred = true;
            while (changeOccurred)
            {
                var seatingPlanAfter = new char[seatingPlanBefore.Length][];
                DeepCopyArray(seatingPlanBefore, seatingPlanAfter);

                occupiedSeats = 0;
                changeOccurred = false;

                for (var horizontalIndex = 0; horizontalIndex < seatingPlanBefore.Length; horizontalIndex++)
                {
                    for (var verticalIndex = 0; verticalIndex < seatingPlanBefore[horizontalIndex].Length; verticalIndex++)
                    {
                        if (seatingPlanBefore[horizontalIndex][verticalIndex].Equals('.'))
                        {
                            continue;
                        }

                        var adjacentSeats = GetAdjacentSeats(seatingPlanBefore, horizontalIndex, verticalIndex);

                        if(seatingPlanBefore[horizontalIndex][verticalIndex].Equals('L'))
                        {
                            if (adjacentSeats.All(s => !s.Equals('#')))
                            {
                                seatingPlanAfter[horizontalIndex][verticalIndex] = '#';
                                changeOccurred = true;
                            }
                        }
                        else
                        {
                            if (adjacentSeats.Count(s => s.Equals('#')) >= 4)
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

                DeepCopyArray(seatingPlanAfter,seatingPlanBefore);
            }

            return occupiedSeats;
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
