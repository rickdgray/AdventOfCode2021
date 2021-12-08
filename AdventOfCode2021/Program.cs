﻿using AdventOfCode2021;

var workingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
var path = Path.Combine(workingDirectory?.Parent?.Parent?.Parent?.FullName ?? throw new DirectoryNotFoundException(), "Data", "day08.txt");
using var fileStream = File.OpenRead(path);
using var streamReader = new StreamReader(fileStream);

var data = new List<string>();
string? line;
while ((line = streamReader.ReadLine()) != null)
{
    data.Add(line);
}

//currently working on:
Console.WriteLine(Day08.Part2(data));