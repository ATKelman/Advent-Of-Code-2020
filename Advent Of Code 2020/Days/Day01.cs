using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    public class Day01 : DayBase
    {
        public Day01(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File.ReadAllLines(_inputPath).Select(x => int.Parse(x)).OrderBy(x => x).ToList();

            var values = (
                from first in input
                from second in input
                where first + second == 2020
                select new
                {
                    first,
                    second
                }).First();

            var result = values.first * values.second;

            return result.ToString();
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllLines(_inputPath).Select(x => int.Parse(x)).OrderBy(x => x).ToList();

            var values = (
                from first in input
                from second in input
                from third in input
                where first + second + third == 2020
                select new
                {
                    first,
                    second, 
                    third
                }).First();

            var result = values.first * values.second * values.third;

            return result.ToString();
        }
    }
}
