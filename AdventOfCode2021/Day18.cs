using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day18
    {
        public static long Part1(List<string> data)
        {
            var line = data.First();
            var postFixStringBuilder = new StringBuilder();
            var stack = new Stack<char>();
            foreach (var digit in line)
            {
                if (char.IsWhiteSpace(digit))
                    continue;

                if (char.IsLetterOrDigit(digit))
                {
                    postFixStringBuilder.Append(digit);
                }

                else if (digit.Equals('('))
                {
                    stack.Push(digit);
                }

                else if (digit.Equals(')'))
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        postFixStringBuilder.Append(stack.Pop());
                    }

                    if (stack.Count > 0 && stack.Peek() != '(')
                    {
                        throw new Exception("invalid");
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
                else
                {
                    while (stack.Count > 0)
                    {
                        postFixStringBuilder.Append(stack.Pop());
                    }
                    stack.Push(digit);
                }
            }

            while (stack.Count > 0)
            {
                postFixStringBuilder.Append(stack.Pop());
            }

            var postFixLine = postFixStringBuilder.ToString();
            Console.WriteLine(postFixLine);

            return -1;
        }

        public static long Part2(List<string> data)
        {
            throw new NotImplementedException();
        }

        public static Node BuildExpressionTree(string line, GroupingNode currentNode)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (int.TryParse(line[i].ToString(), out int num))
                {
                    var op = Operator.None;
                    if (i + 2 < line.Length)
                    {
                        op = (line[i]) switch
                        {
                            '+' => Operator.Addition,
                            '*' => Operator.Multiplication,
                            _ => throw new Exception("invalid operator"),
                        };
                    }

                    currentNode.Children.Add(new NumberNode
                    {
                        NextOperator = op,
                        Value = num
                    });
                }
                else
                {
                    switch (line[i])
                    {
                        case '(':
                            //recuse
                            break;
                        case ')':
                            //base case
                            break;
                        default:
                            break;
                    }
                }
            }

            return new NumberNode();
        }
    }

    public abstract class Node
    {
        public int Value { get; set; }
        public Operator NextOperator { get; set; }
    }

    public class GroupingNode : Node
    {
        public List<Node> Children { get; set; }
    }

    public class NumberNode : Node
    {

    }

    public enum Operator
    {
        None,
        Addition,
        Multiplication
    }
}