using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2020.Days
{
    public class Day16 : DayBase
    {
        public Day16(int day)
            : base(day)
        {

        }

        public override string SolvePart1()
        {
            var input = File.ReadAllLines(_inputPath).ToList();

            var validValues = new HashSet<int>();
            for (int i = 0; i < 19; i++)
            {
                var instance = input[i].Split(new string[] { " ", "-" }, StringSplitOptions.RemoveEmptyEntries).ToList().Where(x => int.TryParse(x, out int y)).Select(x => int.Parse(x)).ToList();

                for (int x = 0; x < instance.Count(); x += 2)
                    validValues.UnionWith(Enumerable.Range(instance[x], (instance[x + 1] - instance[x] + 1)));
            }

            var invalidValues = new List<int>();
            for (int i = 25; i < input.Count(); i++)
            {
                var instance = input[i].Split(',').Select(x => int.Parse(x)).ToList();
                foreach (var value in instance)
                {
                    if (!validValues.Contains(value))
                        invalidValues.Add(value);
                }
            }

            return invalidValues.Sum().ToString();
        }

        record Rule(string name, HashSet<int> validValues);

        public override string SolvePart2()
        {
            var input = File.ReadAllText(_inputPath).Split("\r\n\r\n").ToList().Select(x => x.Split("\r\n")).ToArray();

            var rulesInput = input[0];
            var myTicket = input[1][1];
            var otherTickets = input[2].Skip(1).ToList();

            var allValidValues = new HashSet<int>();

            // Get all rules
            var rules = new List<(string name, HashSet<int> validValues)>();
            foreach (var rule in rulesInput)
            {
                var instance = Regex.Match(rule, "(?<name>.*): (?<value1>\\d*)-(?<value2>\\d*) or (?<value3>\\d*)-(?<value4>\\d*)");
                var name = instance.Groups["name"].Value;
                
                var validValues = new HashSet<int>();
                for (int i = 1; i < 4; i += 2)
                    validValues.UnionWith(Enumerable.Range(int.Parse(instance.Groups[$"value{i}"].Value), (int.Parse(instance.Groups[$"value{i + 1}"].Value) - int.Parse(instance.Groups[$"value{i}"].Value) + 1)));

                allValidValues.UnionWith(validValues);
                rules.Add((name, validValues));
            }

            // Get all valid tickets
            var validTickets = new List<string>();
            foreach (var ticket in otherTickets)
            {
                var instance = ticket.Split(',').Select(x => int.Parse(x)).ToList();
                var validTicket = true;
                foreach (var value in instance)
                {
                    if (!allValidValues.Contains(value))
                        validTicket = false;
                }

                if (validTicket)
                    validTickets.Add(ticket);
            }
            validTickets.Add(myTicket);

            // Take apart ticket and group by columns 
            var ticketValues = new HashSet<int>[rules.Count];
            validTickets.ForEach(x =>
            {
                x.Split(',').Select((x, i) => new { index = i, value = int.Parse(x) }).ToList().ForEach(y =>
                {
                    if (ticketValues[y.index] == null)
                        ticketValues[y.index] = new HashSet<int>();
                    ticketValues[y.index].Add(y.value);
                });
            });

            // Find what columns work for what rule
            var ticketOrder = new List<string>[ticketValues.Count()];
            for (int i = 0; i < ticketValues.Length; i++)
            {
                ticketOrder[i] = new List<string>();

                foreach (var r in rules)
                {
                    var invalidValues = ticketValues[i].Where(x => !r.validValues.Contains(x));
                    if (!invalidValues.Any())
                        ticketOrder[i].Add(r.name);
                    else
                        Console.WriteLine(i + " " + r.name + " " + string.Join(',', invalidValues));
                }         
            }

            List<(int index, string name)> finalTicket = new List<(int index, string name)>();
            while (ticketOrder.Any(x => x.Count >= 1))
            {
                var first = ticketOrder.ToList().Select((x, i) => new { value = x, index = i }).Where(x => x.value.Count == 1);
                if (first.Any())
                {
                    var valueCopy = first.First().value[0];
                    finalTicket.Add((first.First().index, valueCopy));
                    var toRemove = ticketOrder.Where(x => x.Contains(valueCopy)).ToList();
                    foreach (var tr in toRemove)
                        tr.Remove(valueCopy);
                }
                else
                    break;
            }

            var departures = finalTicket.Where(x => x.name.StartsWith("departure")).ToList();

            var myTicketValues = myTicket.Split(',').Select(x => int.Parse(x)).ToArray();
            long product = 1;
            departures.ForEach(x =>
            {
                product *= myTicketValues[x.index];
            });

            return product.ToString();
        }
    }
}
