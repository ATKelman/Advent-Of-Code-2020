using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    class Day09 : DayBase
    {
        List<long> _input;

        public Day09(int day)
            : base(day)
        {
            _input = File.ReadAllLines(_inputPath).Select(x => long.Parse(x)).ToList();
        }

        public override string SolvePart1()
        {
            return GetInvalidNumber().ToString();
        }

        public override string SolvePart2()
        {
            var value = GetInvalidNumber();

            for (int i = 0; i < _input.Count(); i++)
            {
                var sum = _input[i];
                (long min, long max) values = (_input[i], _input[i]);
                for (int j = i +1; j < _input.Count(); j++)
                {
                    if (_input[j] > values.max)
                        values.max = _input[j];
                    else if (_input[j] < values.min)
                        values.min = _input[j];

                    sum += _input[j];
                    if (sum == value)
                        return (values.min + values.max).ToString();
                    else if (sum > value)
                        break;
                }
            }

            return "No Result Found.";
        }

        private long GetInvalidNumber()
        {
            var sums = new List<long>();
            for (int i = 25; i < _input.Count(); i++)
            {
                var instance = _input[i];

                for (int j = i - 25; j < i - 1; j++)
                {
                    for (int x = 1; x < 25; x++)
                        sums.Add(_input[j] + _input[j + x]);
                }

                if (!sums.Contains(instance))
                    return instance;

                sums.Clear();
            }
            return 0;
        }
    }
}
