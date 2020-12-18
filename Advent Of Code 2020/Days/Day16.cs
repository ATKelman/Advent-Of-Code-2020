using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    public class Day16 : DayBase
    {
        public Day16(int day)
            : base(day)
        {

        }

        public override string SolvePart1()
        {
            var input = File.ReadAllLines(_inputPath).ToList();

            var validValues = new HashSet<int>();
            for (int i = 0; i < 19; i++)
            {
                var instance = input[i].Split(new string[] { " ", "-" }, StringSplitOptions.RemoveEmptyEntries).ToList().Where(x => int.TryParse(x, out int y)).Select(x => int.Parse(x)).ToList();

                for (int x = 0; x < instance.Count(); x += 2)
                    validValues.UnionWith(Enumerable.Range(instance[x], (instance[x + 1] - instance[x] + 1)));
            }

            var invalidValues = new List<int>();
            for (int i = 25; i < input.Count(); i++)
            {
                var instance = input[i].Split(',').Select(x => int.Parse(x)).ToList();
                foreach (var value in instance)
                {
                    if (!validValues.Contains(value))
                        invalidValues.Add(value);
                }
            }

            return invalidValues.Sum().ToString();
        }

        public override string SolvePart2()
        {
            return "";
        }
    }
}
