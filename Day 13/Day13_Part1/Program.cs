﻿using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        int totalCost = 0;
        int prizeCount = 0;

        for (int i = 0; i < lines.Length; i += 4)
        {
            // Parse A and B button moves
            var aParts = lines[i].Split(new[] { "X+", ", Y+" }, StringSplitOptions.RemoveEmptyEntries);
            var bParts = lines[i + 1].Split(new[] { "X+", ", Y+" }, StringSplitOptions.RemoveEmptyEntries);
            var pParts = lines[i + 2].Split(new[] { "X=", ", Y=" }, StringSplitOptions.RemoveEmptyEntries);

            int ax = int.Parse(aParts[1]);
            int ay = int.Parse(aParts[2]);
            int bx = int.Parse(bParts[1]);
            int by = int.Parse(bParts[2]);
            int px = int.Parse(pParts[1]);
            int py = int.Parse(pParts[2]);

            int minCost = int.MaxValue;
            bool solvable = false;

            for (int a = 0; a <= 100; a++)
            {
                for (int b = 0; b <= 100; b++)
                {
                    int dx = a * ax + b * bx;
                    int dy = a * ay + b * by;
                    if (dx == px && dy == py)
                    {
                        int cost = a * 3 + b;
                        if (cost < minCost)
                        {
                            minCost = cost;
                            solvable = true;
                        }
                    }
                }
            }

            if (solvable)
            {
                totalCost += minCost;
                prizeCount++;
            }
        }

        Console.WriteLine("Total Prizes Won: " + prizeCount);
        Console.WriteLine("Total Minimum Token Cost: " + totalCost);
    }
}
