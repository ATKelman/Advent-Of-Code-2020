using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2020.Days
{
    public class Day07 : DayBase
    {
        private List<string[]> _input;

        public Day07(int day)
           : base(day)
        {
            _input = File.ReadAllLines(_inputPath).Select(x => x.Split(new string[] { " bags contain" }, StringSplitOptions.RemoveEmptyEntries)).ToList();
        }

        public override string SolvePart1()
        {
            var bags = new HashSet<string>();
            var bagSearch = _input.Where(x => x[1].Contains("shiny gold")).ToList();
            while (bagSearch.Count() > 0)
            {
                var current = bagSearch[0];
                bagSearch.RemoveAt(0);
                bags.Add(current[0]);

                var newBags = _input.Where(x => x[1].Contains(current[0])).ToList();
                bagSearch.AddRange(newBags);
            }

            return bags.Count().ToString();
        }

        public override string SolvePart2()
        {
            var count = 0;
            _input.Where(x => x[0].Contains("shiny gold")).ToList().ForEach(x => 
            {
                count += BagsInBag(x);
            });

            return count.ToString();
        }

        private int BagsInBag(string[] bag)
        {
            var bagsInBag = Regex.Matches(bag[1], "[0-9] \\w* \\w*");
            int sum = 0;
            foreach (var b in bagsInBag)
            {
                var amount = int.Parse(b.ToString().Substring(0, 1));
                var bagName = b.ToString().Substring(2, b.ToString().Length - 2);

                var newBag = _input.Where(x => x[0].Contains(bagName)).ToList().First();
                sum += amount + (amount * BagsInBag(newBag));
            }

            return sum;
        }
    }
}
