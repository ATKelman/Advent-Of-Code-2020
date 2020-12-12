using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2020.Days
{
    public class Day12 : DayBase
    {
        private List<int> seatIds = new List<int>();

        public Day12(int day)
            : base(day)
        {

		}

		public override string SolvePart1()
        {
			(int xPos, int yPos) movement = (0, 0);
			List<(int x, int y)> directions = new List<(int, int)>() 
			{
				(-1, 0), (0, 1), (1, 0), (0, -1) // West, North, East, South
			}; 
			int currentDirection = 2; // Facing east

			var input = File.ReadAllLines(_inputPath).ToList();
			foreach (var instance in input)
			{
				var action = instance.Substring(0, 1);
				var value = int.Parse(instance.Substring(1));

				switch (action)
				{
					case "N":
						movement.yPos += value;
						break;
					case "E":
						movement.xPos += value;
						break;
					case "S":
						movement.yPos -= value;
						break;
					case "W":
						movement.xPos -= value;
						break;
					case "R":
						currentDirection = (currentDirection + (value / 90)) % directions.Count();
						break;
					case "L":
						currentDirection = (directions.Count() + currentDirection - (value / 90)) % directions.Count();
						break;
					case "F":
						var moveX = directions[currentDirection].x * value;
						var moveY = directions[currentDirection].y * value;
						movement = (movement.xPos + moveX, movement.yPos + moveY);
						break;
				}
			}				
			return (Math.Abs(movement.xPos) + Math.Abs(movement.yPos)).ToString();
		}

		public override string SolvePart2()
        {
			(int xPos, int yPos) movement = (0, 0);
			(int xPos, int yPos) waypoint = (10, 1);

			var input = File.ReadAllLines(_inputPath).ToList();
			foreach (var instance in input)
			{
				var action = instance.Substring(0, 1);
				var value = int.Parse(instance.Substring(1));
				var rotations = 0;
				switch (action)
				{
					case "N":
						waypoint.yPos += value;
						break;
					case "E":
						waypoint.xPos += value;
						break;
					case "S":
						waypoint.yPos -= value;
						break;
					case "W":
						waypoint.xPos -= value;
						break;
					case "R":
						rotations = (value / 90) % 4;
						break;
					case "L":
						rotations = (4 - value / 90) % 4;
						break;
					case "F":
						var moveX = value * waypoint.xPos;
						var moveY = value * waypoint.yPos;

						movement = (movement.xPos + moveX, movement.yPos + moveY);
						break;
				}

				for (int i = 0; i < rotations; i++)
				{
					var temp = waypoint.xPos;
					waypoint.xPos = waypoint.yPos;
					waypoint.yPos = -temp;
				}
			}
			return (Math.Abs(movement.xPos) + Math.Abs(movement.yPos)).ToString();
		}
    }
}
