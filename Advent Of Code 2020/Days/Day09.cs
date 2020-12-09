using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    class Day09 : DayBase
    {
        List<long> _input;

        public Day09(int day)
            : base(day)
        {
            _input = File.ReadAllLines(_inputPath).Select(x => long.Parse(x)).ToList();
        }

        public override string SolvePart1()
        {
            return "";
        }

        public override string SolvePart2()
        {
            return "";
        }
    }
}
