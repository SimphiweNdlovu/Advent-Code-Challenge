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

        try
        {
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                

            }

           
           
        }
        catch (Exception e)
        {
            Console.WriteLine("Oops! Something went wrong:");
            Console.WriteLine(e.Message);
        }
    }
}