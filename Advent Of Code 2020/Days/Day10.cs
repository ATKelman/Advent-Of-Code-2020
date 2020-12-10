using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    public class Day10 : DayBase
    {
        public Day10(int day)
            : base(day)
        {

        }

        public override string SolvePart1()
        {
            var values = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 1 } }; // Start 3 on 1 as last jolt is a difference of 3
            File.ReadAllLines(_inputPath).Select(x => int.Parse(x)).ToList().OrderBy(x => x).Aggregate(0, (prev, next) => 
            {
                values[next - prev]++;
                return next;
            });

            var result = values[1] * values[3];
            return result.ToString();
        }

        public override string SolvePart2()
        {
            var onesInARow = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 } };
            int rowCount = 0;
            File.ReadAllLines(_inputPath).Select(x => int.Parse(x)).ToList().OrderBy(x => x).Aggregate(0, (prev, next) =>
            {
                var diff = next - prev;
                if (diff == 1)
                    rowCount++;
                else
                {
                    onesInARow[rowCount]++;
                    rowCount = 0;
                }
                return next;
            });
            onesInARow[rowCount]++;

            // Two 1's in a row have 2 options, 3 1's in a row have 4 and 4 1's have 7. There cannot be more that 4 1's in a row.
            var result = Math.Pow(2, onesInARow[2]) * Math.Pow(4, onesInARow[3]) * Math.Pow(7, onesInARow[4]);
            return result.ToString();
        }
    }
}
