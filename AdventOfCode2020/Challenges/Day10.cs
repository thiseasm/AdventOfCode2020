using System;

namespace AdventOfCode2020.Challenges
{
    public class Day10 : Day
    {
        private readonly int[] _inputs;

        public Day10()
        {
            _inputs = ReadIntegerFile("Day10.txt");
        }

        public override void Start()
        {
            var joltageProduct = CalculateJoltageProduct();

            Console.WriteLine(joltageProduct);
        }

        private int CalculateJoltageProduct()
        {
            var ratings = new int[_inputs.Length + 2];
            Array.Sort(_inputs);
            Array.Copy(_inputs,0,ratings,1,_inputs.Length );

            //Include device ratings
            ratings[^1] = ratings[^2] + 3;

            Array.Sort(ratings);

            var oneJoltDifferences = 0;
            var threeJoltDifferences = 0;

            for (var counter = 1; counter < ratings.Length; counter++)
            {
                if (ratings[counter] - ratings[counter - 1] == 1)
                    oneJoltDifferences++;
                if (ratings[counter] - ratings[counter - 1] == 3)
                    threeJoltDifferences++;
            }

            return oneJoltDifferences * threeJoltDifferences;
        }
    }
}
