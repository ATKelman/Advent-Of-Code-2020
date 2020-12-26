using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2020.Days
{
    class Day22 : DayBase
    {

        public Day22(int day)
            : base(day)
        {
             
        }

        public override string SolvePart1()
        {
            var input = File.ReadAllText(_inputPath).Split("\r\n\r\n").Select(x => x.Split("\r\n").Skip(1).Select(x => int.Parse(x)).ToList()).ToArray();

            var playerOne = new Queue<int>(input[0]);
            var playerTwo = new Queue<int>(input[1]);

            while (playerOne.Count > 0 && playerTwo.Count > 0)
            {
                var playerOnesNumber = playerOne.Dequeue();
                var playerTwosNumber = playerTwo.Dequeue();

                if (playerOnesNumber > playerTwosNumber)
                {
                    playerOne.Enqueue(playerOnesNumber);
                    playerOne.Enqueue(playerTwosNumber);
                }
                else
                {
                    playerTwo.Enqueue(playerTwosNumber);
                    playerTwo.Enqueue(playerOnesNumber);
                }
            }

            var winner = (playerOne.Count > 0 ? playerOne : playerTwo);
            return winner.ToArray().Select((x, i) => x * (winner.Count - i)).Sum(x => x).ToString();
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllText(_inputPath).Split("\r\n\r\n").Select(x => x.Split("\r\n").Skip(1).Select(x => int.Parse(x)).ToList()).ToArray();

            var playerOne = new Queue<int>(input[0]);
            var playerTwo = new Queue<int>(input[1]);

            var winner = RecursiveCombat(playerOne, playerTwo);
            return winner.winnerQueue.ToArray().Select((x, i) => x * (winner.winnerQueue.Count - i)).Sum(x => x).ToString();
        }

        private (bool playerOneWon, Queue<int> winnerQueue) RecursiveCombat(Queue<int> playerOne, Queue<int> playerTwo)
        {
            var seen = new HashSet<(string, string)>();

            var gameAlive = true;
            while (gameAlive)
            {
                var playerOnesNumber = playerOne.Dequeue();
                var playerTwosNumber = playerTwo.Dequeue();

                var CurrentDecks = (string.Join("", playerOne.ToList()), string.Join("", playerTwo.ToList()));

                if (seen.Contains(CurrentDecks))
                    return (true, playerOne);
                seen.Add(CurrentDecks);

                var playerOneWonRound = true;
                if (playerOnesNumber <= playerOne.Count() && playerTwosNumber <= playerTwo.Count()) // Sub Game
                    playerOneWonRound = RecursiveCombat(new Queue<int>(playerOne.Take(playerOnesNumber).ToList()), new Queue<int>(playerTwo.Take(playerTwosNumber).ToList())).playerOneWon;
                else if (playerOnesNumber < playerTwosNumber) // Regular Game
                    playerOneWonRound = false;

                if (playerOneWonRound)
                {
                    playerOne.Enqueue(playerOnesNumber);
                    playerOne.Enqueue(playerTwosNumber);
                }
                else
                {
                    playerTwo.Enqueue(playerTwosNumber);
                    playerTwo.Enqueue(playerOnesNumber);
                }

                if (playerOne.Count == 0 || playerTwo.Count == 0)
                    gameAlive = false;
            }

            return (playerOne.Count > 0 ? (true, playerOne) : (false, playerTwo));
        }
    }
}
