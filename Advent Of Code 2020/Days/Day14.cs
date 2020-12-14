using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    public class Day14 : DayBase
    {
        private List<int> seatIds = new List<int>();

        public Day14(int day)
            : base(day)
        {
            
        }

        public override string SolvePart1()
        {
            var input = File.ReadAllLines(_inputPath).ToList();
            
            var memory = new Dictionary<string, long>();
            var mask = "";

            foreach (var instance in input)
            {
                if (instance.StartsWith("mask"))
                    mask = instance.Split(new string[] { "=", " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                else
                {
                    var values = instance.Split(new string[] { "[", "]", "=", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var binary = Convert.ToString(long.Parse(values[2]), 2).PadLeft(36, '0');
                    var binaryWithMask = binary.Select((x, i) => mask[i] != 'X' ? mask[i] : x);
                    memory[values[1]] = Convert.ToInt64(new string(binaryWithMask.ToArray()), 2);
                }
            }
            return memory.Values.Sum().ToString();
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllLines(_inputPath).ToList();

            var memory = new Dictionary<string, long>();
            var mask = "";

            foreach (var instance in input)
            {
                if (instance.StartsWith("mask"))
                    mask = instance.Split(new string[] { "=", " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                else
                {
                    var values = instance.Split(new string[] { "[", "]", "=", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var binary = Convert.ToString(long.Parse(values[1]), 2).PadLeft(36, '0');
                    var binaryWithMask = binary.Select((x, i) => mask[i] == '0' ? x : mask[i]).ToList();

                    var floating = Enumerable.Range(0, binaryWithMask.Count()).Where(i => binaryWithMask[i] == 'X').ToList();
                    for (long i = 0; i < Math.Pow(2, floating.Count()); i++)
                    {
                        var current = Convert.ToString(i, 2).PadLeft(floating.Count(), '0');
                        for (int j = 0; j < current.Length; j++)
                        {
                            binaryWithMask[floating[j]] = current[j];
                        }

                        memory[new string(binaryWithMask.ToArray())] = long.Parse(values[2]);
                    }
                }
            }

            return memory.Values.Sum().ToString();
        }
    }
}
