﻿using System;
using System.IO;
using System.Collections.Generic;

class Program {
    static void Main() {
        string path = "input.txt";
        if (!File.Exists(path)) {
            Console.WriteLine("Missing input file.");
            return;
        }

        string input = File.ReadAllText(path).Trim();
        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<string> stones = new List<string>(parts);

        for (int step = 0; step < 25; step++) {
            List<string> next = new List<string>();
            foreach (string stone in stones) {
                if (stone == "0") {
                    next.Add("1");
                }
                else if (stone.Length % 2 == 0) {
                    int mid = stone.Length / 2;
                    string left = stone.Substring(0, mid).TrimStart('0');
                    string right = stone.Substring(mid).TrimStart('0');
                    next.Add(string.IsNullOrEmpty(left) ? "0" : left);
                    next.Add(string.IsNullOrEmpty(right) ? "0" : right);
                }
                else {
                    long number = long.Parse(stone);
                    next.Add((number * 2024).ToString());
                }
            }
            stones = next;
        }

        Console.WriteLine(stones.Count);
    }
}