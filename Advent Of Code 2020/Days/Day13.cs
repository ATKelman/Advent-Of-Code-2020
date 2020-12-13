using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    public class Day13 : DayBase
    {
        public Day13(int day)
            : base(day)
        {

        }

        public override string SolvePart1()
        {
            var input = File.ReadAllLines(_inputPath).ToList();
            var departTime = int.Parse(input[0]);
            var buses = input[1].Replace('x', ',').Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

            (int id, int minWaited) earliestBus = (0, int.MaxValue);
            foreach (var bus in buses)
            {
                var timeWaited = bus - (departTime % bus);
                if (timeWaited < earliestBus.minWaited)
                    earliestBus = (bus, timeWaited);
            }

            return (earliestBus.minWaited * earliestBus.id).ToString();
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllLines(_inputPath).ToList()[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                .ToArray()
                .Select((v, index) => new { index, v })
                .Where(x => x.v != "x")
                .Select(x => new { x.index, value = int.Parse(x.v) })
                .ToList();

            long timestamp = 0;
            long timeStep = input[0].value;
            for (var i = 1; i < input.Count(); i++)
            {
                while (true)
                {
                    timestamp += timeStep;
                    if ((timestamp + input[i].index) % input[i].value == 0)
                    {
                        timeStep *= input[i].value;
                        break;
                    }
                }
            }

            return timestamp.ToString();
        }
    }
}
