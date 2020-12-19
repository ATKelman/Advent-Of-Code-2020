using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2020.Days
{
    public class Day18 : DayBase
    {
        public Day18(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            ulong sum = 0;
            foreach (var line in File.ReadAllLines(_inputPath).ToList())
                sum += ulong.Parse(EvaluateExpression(line, new string[] { "\\d+ (\\+|\\*) \\d+"  }));
            
            return sum.ToString();
        }

        public override string SolvePart2()
        {
            ulong sum = 0;
            foreach (var line in File.ReadAllLines(_inputPath).ToList())
                sum += ulong.Parse(EvaluateExpression(line, new string[] { "\\d+ \\+ \\d+", "\\d+ \\* \\d+" }));

            return sum.ToString();
        }

        private string EvaluateExpression(string input, string[] operationPatterns)
        {
            Regex pattern = new Regex("[()]");
            MatchCollection matches;
            while ((matches = Regex.Matches(input, "\\([^\\(\\)]*\\)")).Any())
                input = input.Replace(matches.First().Value, EvaluateExpression(pattern.Replace(matches.First().Value, string.Empty), operationPatterns));

            foreach (var opPattern in operationPatterns)
            {
                while ((matches = Regex.Matches(input, opPattern)).Any())
                {
                    var values = matches.First().Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                    Regex p = new Regex($"{values[0]} \\{values[1]} {values[2]}");
                    input = p.Replace(input, CalculateOperation(values[0], values[2], values[1]), 1);
                }
            }
                     
            return input;
        }

        private string CalculateOperation(string value1, string value2, string op)
        {        
            return op switch
            {
                "*" => (ulong.Parse(value1) * ulong.Parse(value2)).ToString(),
                "+" => (ulong.Parse(value1) + ulong.Parse(value2)).ToString(),
                _ => throw new ArgumentException("Invalid operator")
            };
        }
    }
}
