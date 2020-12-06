using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2020.Challenges
{
    public class Day5 : Day
    {
        private readonly string[] _inputs;

        public Day5()
        {
            _inputs = ReadFile("Day5.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var highestSeatId = GetHighestSeatId();
            var ownSeatId = GetOwnSeatId();

            Console.WriteLine($"The highest seat ID is: {highestSeatId}");
            Console.WriteLine($"The own seat ID is: {ownSeatId}");
        }

        private int GetOwnSeatId()
        {
            var seatIds = CalculateAllSeatIds();
            for (var index = 1; index < seatIds.Count; index++)
            {
                var previousSeatId = seatIds[index-1];
                var thisSeat = seatIds[index];
                if (thisSeat - previousSeatId == 2)
                {
                    return thisSeat - 1;
                }
            }

            return 0;
        }

        private List<int> CalculateAllSeatIds()
        {
            var seatIds = new List<int>();
            foreach (var boardingPass in _inputs)
            {
                var rowPosition = boardingPass.Substring(0, 7);
                var columnPosition = boardingPass.Substring(7);

                var rowNumber = GetRowNumber(rowPosition);
                var columnNumber = GetColumnNumber(columnPosition);
                var seatId = rowNumber * 8 + columnNumber;

                seatIds.Add(seatId);
            }

            seatIds.Sort();
            return seatIds;
        }

        private int GetHighestSeatId()
        {
            var seatIds = CalculateAllSeatIds();
            return seatIds[^1];
        }

        private static int GetRowNumber(string rowPosition)
        {
            var rowNumber = new List<int>();
            for (var index = 0; index < 128; index++)
            {
                rowNumber.Add(index);
            }

            foreach (var character in rowPosition)
            {
                rowNumber = character switch
                {
                    'F' => rowNumber.GetRange(0, rowNumber.Count / 2),
                    'B' => rowNumber.GetRange(rowNumber.Count / 2, rowNumber.Count / 2),
                    _ => throw new IndexOutOfRangeException()
                };
            }

            return rowNumber[0];
        }

        private static int GetColumnNumber(string columnPosition)
        {
            var columnNumber = new List<int>();
            for (var index = 0; index < 8; index++)
            {
                columnNumber.Add(index);
            }

            foreach (var character in columnPosition)
            {
                columnNumber = character switch
                {
                    'L' => columnNumber.GetRange(0, columnNumber.Count / 2),
                    'R' => columnNumber.GetRange(columnNumber.Count / 2, columnNumber.Count / 2),
                    _ => throw new IndexOutOfRangeException()
                };
            }

            return columnNumber[0];
        }

        
    }
}
