﻿using System;
using System.IO;

class Program
{
    static void Main()
    {
        var path = "input.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine("Missing input file.");
            return;
        }

        var aList = new IntList();
        var bList = new IntList();

        try
        {
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                

                if (parts.Length != 2)
                    continue;

                if (int.TryParse(parts[0], out int a) && int.TryParse(parts[1], out int b))
                {
                    aList.Add(a);
                    bList.Add(b);
                }
            }

            aList.Sort();
            bList.Sort();

            long total = 0;
            for (int i = 0; i < aList.Count; i++)
            {
                total += Math.Abs(aList[i] - bList[i]);
            }

            Console.WriteLine("Total distance: " + total);
        }
        catch (Exception e)
        {
            Console.WriteLine("Oops! Something went wrong:");
            Console.WriteLine(e.Message);
        }
    }
}