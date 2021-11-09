using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day12
    {
        public static long Part1(List<string> data)
        {
            double x = 0;
            double y = 0;
            double radians = 0;

            foreach (var line in data)
            {
                var instruction = line.Substring(0, 1);
                var distance = int.Parse(line.Substring(1));

                switch (instruction)
                {
                    case "N":
                        y += distance;
                        break;
                    case "S":
                        y -= distance;
                        break;
                    case "E":
                        x += distance;
                        break;
                    case "W":
                        x -= distance;
                        break;
                    case "L":
                        radians += distance * Math.PI / 180;
                        break;
                    case "R":
                        radians -= distance * Math.PI / 180;
                        break;
                    case "F":
                        x += distance * Math.Cos(radians);
                        y += distance * Math.Sin(radians);
                        break;
                }
            }

            return Convert.ToInt64(Math.Round(Math.Abs(x) + Math.Abs(y), MidpointRounding.AwayFromZero));
        }

        public static long Part2(List<string> data)
        {
            double waypointX = 10;
            double waypointY = 1;

            double shipX = 0;
            double shipY = 0;

            foreach (var line in data)
            {
                var instruction = line.Substring(0, 1);
                var distance = int.Parse(line.Substring(1));

                switch (instruction)
                {
                    case "N":
                        waypointY += distance;
                        break;
                    case "S":
                        waypointY -= distance;
                        break;
                    case "E":
                        waypointX += distance;
                        break;
                    case "W":
                        waypointX -= distance;
                        break;
                    case "L":
                        {
                            switch (distance)
                            {
                                case 90:
                                    {
                                        var tmp = waypointX;
                                        waypointX = -1 * waypointY;
                                        waypointY = tmp;
                                        break;
                                    }
                                case 180:
                                    {
                                        waypointX = -1 * waypointX;
                                        waypointY = -1 * waypointY;
                                        break;
                                    }
                                case 270:
                                    {
                                        var tmp = waypointX;
                                        waypointX = waypointY;
                                        waypointY = -1 * tmp;
                                        break;
                                    }
                                default:
                                    throw new ArgumentException();
                            }
                            break;
                        }
                    case "R":
                        {
                            switch (distance)
                            {
                                case 90:
                                    {
                                        var tmp = waypointX;
                                        waypointX = waypointY;
                                        waypointY = -1 * tmp;
                                        break;
                                    }
                                case 180:
                                    {
                                        waypointX = -1 * waypointX;
                                        waypointY = -1 * waypointY;
                                        break;
                                    }
                                case 270:
                                    {
                                        var tmp = waypointX;
                                        waypointX = -1 * waypointY;
                                        waypointY = tmp;
                                        break;
                                    }
                                default:
                                    throw new ArgumentException();
                            }
                            break;
                        }
                    case "F":
                        {
                            shipX += distance * waypointX;
                            shipY += distance * waypointY;
                            break;
                        }
                }
            }

            return Convert.ToInt64(Math.Abs(shipX) + Math.Abs(shipY));
        }
    }
}