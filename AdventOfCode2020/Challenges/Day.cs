using System.IO;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public abstract class Day
    {
        public abstract void Start();

        protected string[] ReadFile(string file)
        {
            var inputPath = Path.Combine(Properties.Resources.InputsFolder, file);
            return File.ReadAllLines(inputPath);
        }
        protected int[] ReadIntegerFile(string file)
        {
            var inputPath = Path.Combine(Properties.Resources.InputsFolder, file);
            var inputsRaw = File.ReadAllLines(inputPath);

            return inputsRaw.Select(input => int.Parse(input.Trim())).ToArray();
        }

        protected long[] ReadLongFile(string file)
        {
            var inputPath = Path.Combine(Properties.Resources.InputsFolder, file);
            var inputsRaw = File.ReadAllLines(inputPath);

            return inputsRaw.Select(input => long.Parse(input.Trim())).ToArray();
        }

        protected int[] ReadCsv(string file)
        {
            return ReadSv(file, ',');
        }

        protected int[] ReadSv(string file, char separator)
        {
            var inputPath = Path.Combine(Properties.Resources.InputsFolder, file);
            var inputRaw = File.ReadAllText(inputPath).Split(separator);

            return inputRaw.Select(input => int.Parse(input.Trim())).ToArray();
        }
    }
}