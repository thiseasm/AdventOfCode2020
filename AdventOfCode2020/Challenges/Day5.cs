using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Challenges
{
    public class Day5 : Day
    {
        private readonly string[] _inputs;

        public Day5()
        {
            _inputs = ReadFile("Day5.txt");
        }
        public override void Start()
        {
            var highestSeatId = GetHighestSeatId(_inputs);

            Console.WriteLine($"The highest seat ID is: {highestSeatId}");
        }

        private int GetHighestSeatId(IEnumerable<string> inputs)
        {
            var highestId = 0;
            foreach (var boardingPass in inputs)
            {
                var rowPosition = boardingPass.Substring(0, 7);
                var columnPosition = boardingPass.Substring(7);

                var rowNumber = GetRowNumber(rowPosition);
                var columnNumber = GetColumnNumber(columnPosition);
                var seatId = rowNumber * 8 + columnNumber;

                if (seatId > highestId)
                {
                    highestId = seatId;
                }
            }

            return highestId;
        }

        private static int GetRowNumber(string rowPosition)
        {
            var seatNumbers = new List<int>();
            for (var index = 0; index < 128; index++)
            {
                seatNumbers.Add(index);
            }

            foreach (var character in rowPosition)
            {
                seatNumbers = character switch
                {
                    'F' => seatNumbers.GetRange(0, seatNumbers.Count / 2),
                    'B' => seatNumbers.GetRange(seatNumbers.Count / 2, seatNumbers.Count / 2),
                    _ => throw new IndexOutOfRangeException()
                };
            }

            return seatNumbers[0];
        }

        private static int GetColumnNumber(string columnPosition)
        {
            var rowNumber = new List<int>();
            for (var index = 0; index < 8; index++)
            {
                rowNumber.Add(index);
            }

            foreach (var character in columnPosition)
            {
                rowNumber = character switch
                {
                    'L' => rowNumber.GetRange(0, rowNumber.Count / 2),
                    'R' => rowNumber.GetRange(rowNumber.Count / 2, rowNumber.Count / 2),
                    _ => throw new IndexOutOfRangeException()
                };
            }

            return rowNumber[0];
        }

        
    }
}
