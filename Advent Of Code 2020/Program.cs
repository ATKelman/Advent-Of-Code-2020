using System;
using Advent_Of_Code_2020.Days;

namespace Advent_Of_Code_2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020");

            var day = new Day10(10);

            var result = day.SolvePart1();

            Console.WriteLine(result);

            var result2 = day.SolvePart2();

            Console.WriteLine(result2);

            Console.ReadKey();
        }
    }
}
