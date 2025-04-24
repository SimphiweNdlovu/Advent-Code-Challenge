using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = "input.txt";
        if (!File.Exists(path))
        {
            Console.WriteLine("Missing input file.");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        GardenCalculator calculator = new GardenCalculator();
        int totalCost = calculator.CalculateTotalFenceCost(lines);
        Console.WriteLine(totalCost);
    }
}



















