using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day4
    {
        public static long Part1(List<string> data)
        {
            var passport = new Dictionary<string, string>();
            var validPassportCount = 0;
            foreach (var line in data)
            {
                var tokens = line.Split(" ");

                if (tokens.Length == 1 && string.IsNullOrEmpty(tokens.First()))
                {
                    if (passport.Count == 7)
                        validPassportCount++;

                    passport = new Dictionary<string, string>();
                }
                else
                {
                    foreach (var token in tokens)
                    {
                        var field = token.Split(":");

                        switch (field[0])
                        {
                            case "byr":
                                passport[field[0]] = field[1];
                                break;
                            case "iyr":
                                passport[field[0]] = field[1];
                                break;
                            case "eyr":
                                passport[field[0]] = field[1];
                                break;
                            case "hgt":
                                passport[field[0]] = field[1];
                                break;
                            case "hcl":
                                passport[field[0]] = field[1];
                                break;
                            case "ecl":
                                passport[field[0]] = field[1];
                                break;
                            case "pid":
                                passport[field[0]] = field[1];
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            //validation is triggered by newline
            //so do once last check since file terminates without newline
            if (passport.Count == 7)
                validPassportCount++;

            return validPassportCount;
        }

        public static long Part2(List<string> data)
        {
            var passport = new Dictionary<string, string>();
            var validPassportCount = 0;
            foreach (var line in data)
            {
                var tokens = line.Split(" ");

                if (tokens.Length == 1 && string.IsNullOrEmpty(tokens.First()))
                {
                    if (PassportIsValid(passport))
                        validPassportCount++;

                    passport = new Dictionary<string, string>();
                }
                else
                {
                    foreach (var token in tokens)
                    {
                        var field = token.Split(":");

                        switch (field[0])
                        {
                            case "byr":
                                passport[field[0]] = field[1];
                                break;
                            case "iyr":
                                passport[field[0]] = field[1];
                                break;
                            case "eyr":
                                passport[field[0]] = field[1];
                                break;
                            case "hgt":
                                passport[field[0]] = field[1];
                                break;
                            case "hcl":
                                passport[field[0]] = field[1];
                                break;
                            case "ecl":
                                passport[field[0]] = field[1];
                                break;
                            case "pid":
                                passport[field[0]] = field[1];
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            //validation is triggered by newline
            //so do once last check since file terminates without newline
            if (PassportIsValid(passport))
                validPassportCount++;

            return validPassportCount;
        }

        public static bool PassportIsValid(Dictionary<string, string> passport)
        {
            if (!passport.ContainsKey("byr")
                || !passport.ContainsKey("iyr")
                || !passport.ContainsKey("eyr")
                || !passport.ContainsKey("hgt")
                || !passport.ContainsKey("hcl")
                || !passport.ContainsKey("ecl")
                || !passport.ContainsKey("pid"))
                return false;

            if (int.Parse(passport["byr"]) < 1920 || int.Parse(passport["byr"]) > 2020)
                return false;

            if (int.Parse(passport["iyr"]) < 2010 || int.Parse(passport["iyr"]) > 2020)
                return false;

            if (int.Parse(passport["eyr"]) < 2020 || int.Parse(passport["eyr"]) > 2030)
                return false;

            if (passport["hgt"].Length < 4)
                return false;
            var heightUnit = passport["hgt"][^2..];
            var heightMeasure = int.Parse(passport["hgt"][0..^2]);
            switch (heightUnit)
            {
                case "cm":
                    {
                        if (heightMeasure < 150 || heightMeasure > 193)
                            return false;
                        break;
                    }
                case "in":
                    {
                        if (heightMeasure < 59 || heightMeasure > 76)
                            return false;
                        break;
                    }
                default:
                    return false;
            }

            var hairColor = passport["hcl"];

            if (hairColor.Length != 7)
                return false;

            var matches = new Regex(@"/#[\da-f]{6}/g").Match(hairColor);
            if (matches == null)
                return false;

            var validEyeColors = new List<string>
            {
                "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
            };

            if (!validEyeColors.Contains(passport["ecl"]))
                return false;

            if (passport["pid"].Length != 9
                || !int.TryParse(passport["pid"], out _))
                return false;

            return true;
        }
    }
}