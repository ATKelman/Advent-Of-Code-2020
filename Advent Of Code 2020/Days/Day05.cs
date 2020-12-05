using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    public class Day05 : DayBase
    {
        private List<int> seatIds = new List<int>();

        public Day05(int day)
            : base(day)
        {
            GetSeats();
        }

        public override string SolvePart1()
        {     
            return seatIds.Max().ToString();
        }

        public override string SolvePart2()
        {
            var mySeatId = 0;
            var seat = seatIds.OrderBy(x => x).Aggregate((prev, next) =>
            {
                if ((next - prev) == 2)
                    mySeatId = prev + 1;
                return next;
            });

            return mySeatId.ToString();
        }

        private void GetSeats()
        {
            var input = File.ReadAllLines(_inputPath);

            foreach (var seat in input)
            {
                var row = Enumerable.Range(0, 128).ToList();
                var col = Enumerable.Range(0, 8).ToList();

                foreach (var i in seat)
                {
                    if (i == 'F')
                        row = row.Take(row.Count() / 2).ToList();
                    else if (i == 'B')
                        row = row.Skip(row.Count() / 2).Take(row.Count() / 2).ToList();
                    else if (i == 'L')
                        col = col.Take(col.Count() / 2).ToList();
                    else if (i == 'R')
                        col = col.Skip(col.Count() / 2).Take(col.Count() / 2).ToList();
                }

                var seatId = row[0] * 8 + col[0];
                seatIds.Add(seatId);
            }
        }
    }
}
