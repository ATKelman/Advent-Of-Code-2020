using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    public class Day03 : DayBase
    {
        private string[] _input;

        public Day03(int day)
            : base(day)
        {

        }

        public override string SolvePart1()
        {
            _input = File.ReadAllLines(_inputPath);

            var treesHit = CalculatePath(3, 1);

            return $"Trees hit: {treesHit}";
        }

        public override string SolvePart2()
        {
            var slopes = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(3, 1),
                new Tuple<int, int>(5, 1),
                new Tuple<int, int>(7, 1),
                new Tuple<int, int>(1, 2),
            };

            BigInteger treesHit = 1;
            foreach (var slope in slopes)
                treesHit *= CalculatePath(slope.Item1, slope.Item2);

            return $"Trees hit: {treesHit}";
        }

        private int CalculatePath(int stepsRight, int stepsDown)
        {
            var xPos = 0;
            var yPos = 0;

            bool insideMap = true;
            int treesHit = 0;
            while (insideMap)
            {
                xPos = (xPos + stepsRight) % _input[yPos].Length;
                yPos += stepsDown;

                if (yPos >= _input.Count())
                    insideMap = false;
                else if (_input[yPos][xPos] == '#')
                    treesHit++;
            }

            return treesHit;
        }
    }
}
