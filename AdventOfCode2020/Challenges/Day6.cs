using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2020.Challenges
{
    public class Day6 : Day
    {
        private readonly string[] _inputs;

        public Day6()
        {
            _inputs = ReadFile("Day6.txt");
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        public override void Start()
        {
            var positiveAnswers = GetPositiveAnswersFromGroups();
            var uniformPositiveAnswers = GetUniformPositiveAnswers();

            Console.WriteLine($"The sum of all positively answered questions is: {positiveAnswers}");
            Console.WriteLine($"The sum of all uniform positively answered questions is: {uniformPositiveAnswers}");
        }

        private int GetUniformPositiveAnswers()
        {
            var positiveAnswers = 0;
            var groupsAnsweredQuestions =  string.Empty;

            for (var counter = 0; counter < _inputs.Length; counter++)
            {
                if(groupsAnsweredQuestions.Equals(string.Empty))
                {
                    groupsAnsweredQuestions += _inputs[counter];
                }
                else
                {
                    var answersIntersection = groupsAnsweredQuestions.Intersect(_inputs[counter]).ToArray();
                    groupsAnsweredQuestions = new string(answersIntersection);

                    if (groupsAnsweredQuestions.Equals(string.Empty))
                    {
                        //Skip current group
                        do
                        {
                            counter++;
                        } while (!_inputs[counter].Equals(string.Empty));
                    }
                }

                if (counter + 1 != _inputs.Length && !_inputs[counter + 1].Equals(string.Empty))
                    continue;

                positiveAnswers += groupsAnsweredQuestions.Select(q => q).Count();
                groupsAnsweredQuestions = string.Empty;
            }

            return positiveAnswers;
        }

        private int GetPositiveAnswersFromGroups()
        {
            var positiveAnswers = 0;
            var groupsAnsweredQuestions = string.Empty;

            for (var counter = 0; counter < _inputs.Length; counter++)
            {
                groupsAnsweredQuestions += _inputs[counter];

                if (counter + 1 != _inputs.Length && !_inputs[counter + 1].Equals(string.Empty))
                    continue;

                positiveAnswers += groupsAnsweredQuestions.Select(q => q).Distinct().Count();
                groupsAnsweredQuestions = string.Empty;
            }

            return positiveAnswers;
        }
    }
}
