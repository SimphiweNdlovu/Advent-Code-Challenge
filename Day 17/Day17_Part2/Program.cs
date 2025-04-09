using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");
        int[] program = Array.Empty<int>();
        int expectedA = 0; // This would be derived based on the problem's specific input

        foreach (string line in lines)
        {
            if (line.StartsWith("Program: "))
            {
                string[] parts = line.Substring("Program: ".Length).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                program = new int[parts.Length];
                for (int i = 0; i < parts.Length; i++)
                    program[i] = int.Parse(parts[i].Trim());
            }
        }

        // The minimal A is the program's reversed digits treated as a base-8 number
        long minimalA = 0;
        for (int i = program.Length - 1; i >= 0; i--)
        {
            minimalA = minimalA * 8 + program[i];
        }

        Console.WriteLine(minimalA);
    }
}