using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class Day14
    {
        public static long Part1(List<string> data)
        {
            var mask = string.Empty;
            var mem = new Dictionary<long, long>();
            foreach (var line in data)
            {
                var instruction = line.Split(" ")[0];
                var value = line.Split(" ")[2];

                if (instruction.Substring(0, 4).Equals("mask"))
                {
                    mask = value;
                }
                else
                {
                    //I am not proud of this
                    var address = long.Parse(instruction.Split("[")[1].Split("]")[0]);

                    var valueToMask = Convert.ToString(long.Parse(value), 2).PadLeft(36, '0');

                    for (int i = 0; i < mask.Length; i++)
                    {
                        switch (mask[i])
                        {
                            case '0':
                                valueToMask = valueToMask.Remove(i, 1).Insert(i, "0");
                                break;
                            case '1':
                                valueToMask = valueToMask.Remove(i, 1).Insert(i, "1");
                                break;
                            default:
                                break;
                        }
                    }

                    if (mem.ContainsKey(address))
                    {
                        mem[address] = Convert.ToInt64(valueToMask, 2);
                    }
                    else
                    {
                        mem.Add(address, Convert.ToInt64(valueToMask, 2));
                    }
                }
            }

            var total = 0L;
            foreach (var (add, val) in mem)
            {
                total += val;
            }

            return total;
        }

        public static long Part2(List<string> data)
        {
            var mask = string.Empty;
            var mem = new Dictionary<long, long>();
            foreach (var line in data)
            {
                var instruction = line.Split(" ")[0];
                var value = line.Split(" ")[2];

                if (instruction.Substring(0, 4).Equals("mask"))
                {
                    mask = value;
                }
                else
                {
                    //I am not proud of this
                    var addressToMask = Convert.ToString(long.Parse(instruction.Split("[")[1].Split("]")[0]), 2).PadLeft(36, '0');

                    var stringBuilderList = new List<StringBuilder>
                    {
                        new StringBuilder(36)
                    };
                    for (int i = 0; i < 36; i++)
                    {
                        switch (mask[i])
                        {
                            case '0':
                                {
                                    foreach (var stringBuilder in stringBuilderList)
                                    {
                                        stringBuilder.Append(addressToMask[i]);
                                    }
                                    break;
                                }
                            case '1':
                                {
                                    foreach (var stringBuilder in stringBuilderList)
                                    {
                                        stringBuilder.Append('1');
                                    }
                                    break;
                                }
                            case 'X':
                                {
                                    var stringBuilderListCount = stringBuilderList.Count;
                                    //make copies of all current
                                    for (int j = 0; j < stringBuilderListCount; j++)
                                    {
                                        stringBuilderList.Add(new StringBuilder(stringBuilderList[j].ToString(), 36));
                                    }

                                    //first half get 0 appended
                                    for (int j = 0; j < stringBuilderListCount; j++)
                                    {
                                        stringBuilderList[j].Append('0');
                                    }

                                    //second half get 1 appended
                                    for (int j = stringBuilderListCount; j < stringBuilderList.Count; j++)
                                    {
                                        stringBuilderList[j].Append('1');
                                    }

                                    break;
                                }
                        }
                    }

                    var addresses = new List<long>();
                    foreach (var stringBuilder in stringBuilderList)
                    {
                        addresses.Add(Convert.ToInt64(stringBuilder.ToString(), 2));
                    }

                    foreach (var address in addresses)
                    {
                        if (mem.ContainsKey(address))
                        {
                            mem[address] = long.Parse(value);
                        }
                        else
                        {
                            mem.Add(address, long.Parse(value));
                        }
                    }
                }
            }

            var total = 0L;
            foreach (var (add, val) in mem)
            {
                total += val;
            }

            return total;
        }
    }
}