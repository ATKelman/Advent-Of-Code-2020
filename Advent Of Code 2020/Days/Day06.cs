﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    public class Day06 : DayBase
    {
        public Day06(int day)
           : base(day)
        {

        }

        public override string SolvePart1()
        {
            var count = 0;
            File.ReadAllText(_inputPath).Split("\r\n\r\n").ToList().ForEach(group => 
            {
                count += group.Replace("\r\n", "").ToCharArray().Distinct().Count();
            });

            return count.ToString();
        }

        public override string SolvePart2()
        {
            var count = 0;
            File.ReadAllText(_inputPath).Split("\r\n\r\n").ToList().ForEach(group =>
            {
                count += group.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray()).Aggregate<IEnumerable<char>>((prev, next) => prev.Intersect(next)).ToList().Count();
            });

            return count.ToString();
        }
    }
}
