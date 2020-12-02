using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    public class Day02 : DayBase
    {
        public Day02(int day)
            : base(day)
        {

        }

        public override string SolvePart1()
        {
            var input = File.ReadAllLines(_inputPath);

            var correctPasswords = 0;
            foreach (var line in input)
            {
                var values = line.Replace('-', ' ').Split(' ');
                var min = int.Parse(values[0]);
                var max = int.Parse(values[1]);

                var codeword = values[2][0];

                var count = values[3].Where(x => x == codeword).Count();
                if (count >= min && count <= max)
                    correctPasswords++;
            }

            return correctPasswords.ToString();
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllLines(_inputPath);

            var correctPasswords = 0;
            foreach (var line in input)
            {
                var values = line.Replace('-', ' ').Split(' ');
                var firstPos = int.Parse(values[0]) - 1;
                var secondPos = int.Parse(values[1]) - 1;

                var codeword = values[2][0];

                var firstLetter = values[3][firstPos];
                var secondLetter = values[3][secondPos];

                if ((firstLetter == codeword && secondLetter != codeword)
                    || (firstLetter != codeword && secondLetter == codeword))
                    correctPasswords++;
            }

            return correctPasswords.ToString();
        }
    }
}
