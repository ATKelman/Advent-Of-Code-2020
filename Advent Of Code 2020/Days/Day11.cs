using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Advent_Of_Code_2020.Days
{
    class Day11 : DayBase
    {
        public Day11(int day)
            : base(day)
        {
            
        }

        public override string SolvePart1()
        {
            var currentSeats = File.ReadAllLines(_inputPath).ToArray();
            while (true)
            {
                var newSeats = CalculateSeats(currentSeats, 4, 1);

                var areEqual = newSeats.SequenceEqual(currentSeats);
                if (areEqual)
                    return string.Join(' ', newSeats).Count(x => x == '#').ToString();

                newSeats.CopyTo(currentSeats, 0);
            }
        }

        public override string SolvePart2()
        {
            var currentSeats = File.ReadAllLines(_inputPath).ToArray();
            while (true)
            {
                var newSeats = CalculateSeats(currentSeats, 5, -1);

                var areEqual = newSeats.SequenceEqual(currentSeats);
                if (areEqual)
                    return string.Join(' ', newSeats).Count(x => x == '#').ToString();

                newSeats.CopyTo(currentSeats, 0);
            }
        }

        private string[] CalculateSeats(string[] seats, int tolerance, int vision)
        {
            var newSeats = new string[seats.Count()];

            for (int row = 0; row < seats.Length; row++)
            {
                var rowSeating = "";
                for (int col = 0; col < seats[row].Length; col++)
                {
                    var occupiedSeats = 0;
                    for (int directionX = -1; directionX <= 1; directionX++)
                    {
                        for (int directionY = -1; directionY <= 1; directionY++)
                        {
                            if (directionX == 0 && directionY == 0)
                                continue;

                            var occupiedSeatFound = false;
                            var x = directionX + col;
                            var y = directionY + row;

                            var sight = vision;

                            while (x >= 0 && x < seats[row].Length
                                && y >= 0 && y < seats.Length
                                && !occupiedSeatFound
                                && sight-- != 0)
                            {
                                if (seats[y][x] != '.')
                                    occupiedSeatFound = true;
                                else
                                {
                                    x += directionX;
                                    y += directionY;
                                }
                            }

                            if (occupiedSeatFound && seats[y][x] == '#')
                                occupiedSeats++;
                        }
                    }

                    if (seats[row][col] == 'L' && occupiedSeats == 0)
                        rowSeating += "#";
                    else if (seats[row][col] == '#' && occupiedSeats >= tolerance)
                        rowSeating += "L";
                    else
                        rowSeating += seats[row][col];
                }
                newSeats[row] = rowSeating;
            }
            return newSeats;
        }
    }
}
