using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2020.Days
{
    public class Day04 : DayBase
    {
        public Day04(int day)
            : base(day)
        {

        }

        public override string SolvePart1()
        {
            var validPassports = 0;
            File.ReadAllText(_inputPath).Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x =>
            {
                var regex = Regex.Matches(x, "eyr|iyr|byr|ecl|pid|hcl|hgt");
                if (regex.Count() == 7)
                    validPassports++;
            });

            return validPassports.ToString();
        }

        public override string SolvePart2()
        {
            var validPassports = 0;
            File.ReadAllText(_inputPath).Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None).ToList().ForEach(x =>
            {
                var regex = Regex.Matches(x, 
                    "eyr:(202[0-9]|2030)" +
                    "|iyr:(201[0-9]|2020)" +
                    "|byr:(19[2-9][0-9]|200[0-2])" +
                    "|ecl:(amb|blu|brn|gry|grn|hzl|oth)" +
                    "|pid:[0-9]{9}" +
                    "|hcl:#[\\w]{6}" +
                    "|hgt:(1([5-8][0-9]|9[0-3])cm|(59|6[0-9]|7[0-6])in)");
                if (regex.Count() == 7)
                    validPassports++;
            });
            return validPassports.ToString();
        }
    }
}
