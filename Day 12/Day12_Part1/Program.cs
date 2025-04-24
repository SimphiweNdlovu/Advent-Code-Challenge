﻿using System;
using System.IO;

class Program {
    static void Main() {
        string path = "input.txt";
        if (!File.Exists(path)) {
            Console.WriteLine("Missing input file.");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        GardenMap garden = new GardenMap(lines);
        int totalCost = garden.CalculateTotalFenceCost();
  
        Console.WriteLine("Total fence cost: " + totalCost);
    }
}

