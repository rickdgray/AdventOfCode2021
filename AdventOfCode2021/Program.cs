using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    public class Program
    {
        static void Main()
        {
            var workingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var path = Path.Combine(workingDirectory.Parent.Parent.Parent.FullName, "Data", "day18.txt");
            using var fileStream = File.OpenRead(path);
            using var streamReader = new StreamReader(fileStream);

            var data = new List<string>();
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                data.Add(line);
            }

            //currently working on:
            Console.WriteLine(Day18.Part1(data));
        }
    }
}
