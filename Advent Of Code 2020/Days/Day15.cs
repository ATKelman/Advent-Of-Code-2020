using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    class Day15 : DayBase
    {
        public Day15(int day)
            : base(day)
        {

        }

        public override string SolvePart1()
        {
            return CalculateNumberSpoken(2020).ToString();
        }

        public override string SolvePart2()
        {
            return CalculateNumberSpoken(30000000).ToString();
        }

        private int CalculateNumberSpoken(int turns)
        {
            var values = new Dictionary<int, int>(); // key = number, value = index of when spoken last

            var input = File.ReadAllText(_inputPath).Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            for (int i = 0; i < input.Count(); i++)
                values.Add(input[i], (i + 1));

            var last = input.Last();
            for (int turn = input.Count(); turn < turns; turn++)
            {
                if (!values.ContainsKey(last))
                {
                    values[last] = turn;
                    last = 0;
                }
                else
                {
                    var diff = turn - values[last];
                    values[last] = turn;
                    last = diff;
                }
            }

            return last;
        }
    }
}
