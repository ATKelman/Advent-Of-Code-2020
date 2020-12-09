using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    class BootCode
    {
        public BootCode()
        {
            VisitedValues = new HashSet<int>();
        }

        public int LastVisitedValue { get; set; }
        public HashSet<int> VisitedValues { get; set; }
        public int Acc { get; set; }
    }

    public class Day08 : DayBase
    {
        string[] _input;

        public Day08(int day)
            : base(day)
        {
            _input = File.ReadAllLines(_inputPath).ToArray();
        }

        public override string SolvePart1()
        {
            return SolveBootCode(_input).Acc.ToString();
        }

        public override string SolvePart2()
        {
            var bootCode = SolveBootCode(_input);

            foreach (var visitedValue in bootCode.VisitedValues)
            {
                var value = _input[visitedValue].Split(' ');
                if (value[0] == "nop" || value[0] == "jmp")
                {
                    string[] modifiedInput = new string[_input.Length];
                    _input.CopyTo(modifiedInput, 0);

                    modifiedInput[visitedValue] = ((value[0] == "nop") ? "jmp" : "nop") + " " + value[1];

                    var modifiedResult = SolveBootCode(modifiedInput);
                    if (modifiedResult.LastVisitedValue == _input.Length - 1)
                        return modifiedResult.Acc.ToString();
                }
            }

            return "No Result Found.";
        }

        private BootCode SolveBootCode(string[] input)
        {
            var result = new BootCode();

            for (int i = 0; i < input.Length; i++)
            {
                result.LastVisitedValue = i;
                if (result.VisitedValues.Contains(i))
                    break;

                result.VisitedValues.Add(i);

                var instance = input[i].Split(' ');
                switch (instance[0])
                {
                    case "nop":
                        break;
                    case "acc":
                        result.Acc += int.Parse(instance[1]);
                        break;
                    case "jmp":
                        i += int.Parse(instance[1]) - 1;
                        break;
                }
            }
            return result;
        }
    }
}
