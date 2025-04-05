using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string input = File.ReadAllText("input.txt");

        string pattern = @"(?<do>do\(\))|(?<dont>don't\(\))|(?<mul>mul\((?<x>\d{1,3}),(?<y>\d{1,3})\))";

        bool enabled = true;
        int sum = 0;

        foreach (Match match in Regex.Matches(input, pattern))
        {
            if (match.Groups["do"].Success)
            {
                enabled = true;
            }
            else if (match.Groups["dont"].Success)
            {
                enabled = false;
            }
            else if (match.Groups["mul"].Success)
            {
                if (enabled)
                {
                    int x = int.Parse(match.Groups["x"].Value);
                    int y = int.Parse(match.Groups["y"].Value);
                    sum += x * y;
                }
            }
        }

        Console.WriteLine(sum);
    }
}