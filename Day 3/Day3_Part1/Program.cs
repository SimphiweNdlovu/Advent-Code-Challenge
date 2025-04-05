using System;
using System.IO;
using System.Text.RegularExpressions;

public class Day3
{
    public static void Main(string[] args)
    {
        var path = "input.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine("Missing input file.");
            return;
        }
        string input = File.ReadAllText(path);
        
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        
        int sum = 0;
        foreach (Match match in Regex.Matches(input, pattern))
        {
            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);
            sum += x * y;
        }
        
        Console.WriteLine(sum);
    }
}