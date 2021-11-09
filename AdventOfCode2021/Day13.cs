using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day13
    {
        public static long Part1(List<string> data)
        {
            var earliestDepart = int.Parse(data.First());
            var buses = data[1]
                .Split(",")
                .Where(n => !n.Equals("x"))
                .Select(n => int.Parse(n))
                .ToList();

            var earliestBusTimes = new List<int>();
            foreach (var bus in buses)
            {
                earliestBusTimes.Add(bus - (earliestDepart % bus) + earliestDepart);
            }

            var earliestBus = earliestBusTimes.Min();

            for (int i = 0; i < earliestBusTimes.Count; i++)
            {
                if (earliestBusTimes[i] == earliestBus)
                {
                    return (earliestBus - earliestDepart) * buses[i];
                }
            }

            return -1;
        }

        public static long Part2(List<string> data)
        {
            var buses = data[1]
                .Split(",")
                .Select(n => n.Equals("x") ? -1 : int.Parse(n))
                .ToList();

            //-1 indicates a space
            var busPositions = new Dictionary<int, int>();
            for (int i = 0; i < buses.Count; i++)
            {
                if (buses[i] != -1)
                {
                    busPositions.Add(buses[i], i);
                }
            }

            //find the largest for reference point
            var largestBus = busPositions.Keys.Max();
            var positionOfLargestBus = busPositions[busPositions.Keys.Max()];

            //calculate offset then separate by if the offset is equal to itself
            var busesWithRemainderZero = new List<int>();
            var busesWithOtherRemainders = new Dictionary<int, int>();
            foreach (var (bus, position) in busPositions)
            {
                if (bus == largestBus)
                    continue;

                if (bus == Math.Abs(positionOfLargestBus - position))
                    busesWithRemainderZero.Add(bus);
                else
                    busesWithOtherRemainders.Add(bus, positionOfLargestBus - position);
            }

            //all buses with offset equal to itself are just multiplied to the largest number
            var startingNumber = busesWithRemainderZero.Aggregate(largestBus, (total, next) => total * next);
            for (long current = startingNumber; true; current += startingNumber)
            {
                if (busesWithOtherRemainders.All(b => (current - b.Value) % b.Key == 0))
                    return current - positionOfLargestBus;
            }
        }
    }
}