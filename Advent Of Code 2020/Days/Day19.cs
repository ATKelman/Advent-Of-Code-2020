using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_Of_Code_2020.Days
{
    public class Day19 : DayBase
    {
        public Day19(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File.ReadAllText(_inputPath).Split("\r\n\r\n").ToList();
            var rules = input[0].Split(Environment.NewLine).Select(x => x.Split(": ")).ToDictionary(y => int.Parse(y[0]), y => y[1]);

            var instance = rules[0];
            while (Regex.IsMatch(instance, "\\d+"))
                instance = Regex.Replace(instance, "\\d+", m => "(" + rules[int.Parse(m.Value)] + ")");

            var pattern = new Regex("^" + instance.Replace(" ", "").Replace("\"", "") + "$");
            return input[1].Split(Environment.NewLine).Count(x => pattern.IsMatch(x)).ToString();
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllText(_inputPath).Split("\r\n\r\n").ToList();
            var rules = input[0].Split(Environment.NewLine).Select(x => x.Split(": ")).ToDictionary(y => int.Parse(y[0]), y => y[1]);
            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";

            while (true)
            {
                var rule = rules.FirstOrDefault(x => !Regex.IsMatch(x.Value, "\\d+") && x.Key != 42 && x.Key != 31);
                if (rule.Equals(default(KeyValuePair<int, string>)))
                    break;

                foreach (var i in rules.Keys.ToArray())
                {
                    rules[i] = Regex.Replace(rules[i], "\\b" + rule.Key + "\\b", "(" + rule.Value + ")");
                }
                rules.Remove(rule.Key);
            }

            rules[31] = rules[31].Replace(" ", "").Replace("\"", "");
            rules[42] = rules[42].Replace(" ", "").Replace("\"", "");

            var pattern = new Regex("^" + rules[0].Replace("8", "(" + rules[42] + ")+").Replace("11", "(?<A>" + rules[42] + ")+(?<-A> " + rules[31] + ")+").Replace(" ", "") + "$");
            return input[1].Split(Environment.NewLine).Count(x => pattern.IsMatch(x)).ToString();
        }
    }
}
