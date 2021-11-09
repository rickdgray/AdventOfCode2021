using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day16
    {
        public static long Part1(List<string> data)
        {
            var rules = new Dictionary<string, Tuple<Tuple<int, int>, Tuple<int, int>>>();
            var myTicket = new List<int>();
            var nearbyTickets = new List<List<int>>();

            var lineNumber = 0;
            for (int i = lineNumber; i < data.Count; i++)
            {
                var line = data[i];

                if (string.IsNullOrEmpty(line))
                {
                    lineNumber = i;
                    break;
                }

                var rule = line.Split(": ");
                var ruleTokens = rule[1].Split(" or ");
                var firstRule = ruleTokens[0].Split("-");
                var secondRule = ruleTokens[1].Split("-");

                rules.Add(rule[0], Tuple.Create(
                    Tuple.Create(int.Parse(firstRule[0]), int.Parse(firstRule[1])),
                    Tuple.Create(int.Parse(secondRule[0]), int.Parse(secondRule[1]))));
            }
            lineNumber += 2;
            myTicket.AddRange(data[lineNumber].Split(",").Select(n => int.Parse(n)).ToList());
            lineNumber += 3;
            for (int i = lineNumber; i < data.Count; i++)
            {
                var line = data[i];

                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                nearbyTickets.Add(new List<int>());
                nearbyTickets.Last().AddRange(data[i].Split(",").Select(n => int.Parse(n)).ToList());
            }

            var invalidFields = new List<int>();
            foreach (var nearbyTicket in nearbyTickets)
            {
                foreach (var field in nearbyTicket)
                {
                    if (rules.All(r => field < r.Value.Item1.Item1
                        || (field > r.Value.Item1.Item2 && field < r.Value.Item2.Item1)
                        || field > r.Value.Item2.Item2))
                    {
                        invalidFields.Add(field);
                    }
                }
            }

            return invalidFields.Sum();
        }

        public static long Part2(List<string> data)
        {
            var rulesLookup = new Dictionary<string, Tuple<Tuple<int, int>, Tuple<int, int>>>();
            var myTicket = new List<int>();
            var nearbyTickets = new List<List<int>>();

            var lineNumber = 0;
            for (int i = lineNumber; i < data.Count; i++)
            {
                var line = data[i];

                if (string.IsNullOrEmpty(line))
                {
                    lineNumber = i;
                    break;
                }

                var rule = line.Split(": ");
                var ruleTokens = rule[1].Split(" or ");
                var firstRule = ruleTokens[0].Split("-");
                var secondRule = ruleTokens[1].Split("-");

                rulesLookup.Add(rule[0], Tuple.Create(
                    Tuple.Create(int.Parse(firstRule[0]), int.Parse(firstRule[1])),
                    Tuple.Create(int.Parse(secondRule[0]), int.Parse(secondRule[1]))));
            }
            lineNumber += 2;
            myTicket.AddRange(data[lineNumber].Split(",").Select(n => int.Parse(n)).ToList());
            lineNumber += 3;
            for (int i = lineNumber; i < data.Count; i++)
            {
                var line = data[i];

                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                nearbyTickets.Add(new List<int>());
                nearbyTickets.Last().AddRange(data[i].Split(",").Select(n => int.Parse(n)).ToList());
            }

            for (int i = 0; i < nearbyTickets.Count; i++)
            {
                foreach (var field in nearbyTickets[i])
                {
                    if (rulesLookup.All(r => field < r.Value.Item1.Item1
                        || (field > r.Value.Item1.Item2 && field < r.Value.Item2.Item1)
                        || field > r.Value.Item2.Item2))
                    {
                        nearbyTickets[i] = null;
                        break;
                    }
                }
            }
            nearbyTickets.RemoveAll(t => t == null);

            var availableColumns = Enumerable.Range(0, nearbyTickets.First().Count).ToList();
            var availableRules = new List<string>(rulesLookup.Keys.ToList());
            var columnAssignments = new Dictionary<string, int>();
            while (columnAssignments.Count < rulesLookup.Count)
            {
                foreach (var i in availableColumns.ToList())
                {
                    var matchingRules = new List<string>(availableRules);
                    foreach (var ticket in nearbyTickets)
                    {
                        foreach (var ruleName in matchingRules.ToList())
                        {
                            var rule = rulesLookup[ruleName];

                            if (ticket[i] < rule.Item1.Item1
                                || (ticket[i] > rule.Item1.Item2 && ticket[i] < rule.Item2.Item1)
                                || ticket[i] > rule.Item2.Item2)
                            {
                                if (matchingRules.Contains(ruleName))
                                    matchingRules.Remove(ruleName);
                            }

                            if (matchingRules.Count == 1)
                                break;
                        }

                        if (matchingRules.Count == 1)
                            break;
                    }

                    if (matchingRules.Count != 1)
                        continue;

                    columnAssignments.Add(matchingRules.First(), i);
                    availableColumns.Remove(i);
                    availableRules.Remove(matchingRules.First());

                    if (availableRules.Count == 1)
                    {
                        columnAssignments.Add(availableRules.First(), i + 1);
                        break;
                    }
                }
            }

            var total = 1L;
            foreach (var (name, column) in columnAssignments)
            {
                if (name.Length > 8 && name.Substring(0, 9).Equals("departure"))
                    total *= myTicket[column];
            }

            return total;
        }
    }
}