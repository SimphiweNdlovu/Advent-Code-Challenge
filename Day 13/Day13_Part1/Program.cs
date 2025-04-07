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
        int totalTokens = 0;
        int prizes = 0;
        int i = 0;

        while (i + 2 < lines.Length) {
            string aLine = lines[i];
            string bLine = lines[i + 1];
            string pLine = lines[i + 2];

            int ax = Get(aLine, "X+");
            int ay = Get(aLine, "Y+");
            int bx = Get(bLine, "X+");
            int by = Get(bLine, "Y+");
            int tx = Get(pLine, "X=");
            int ty = Get(pLine, "Y=");

            int best = 1000000;

            for (int a = 0; a <= 100; a++) {
                for (int b = 0; b <= 100; b++) {
                    int x = a * ax + b * bx;
                    int y = a * ay + b * by;
                    if (x == tx && y == ty) {
                        int cost = a * 3 + b;
                        if (cost < best) best = cost;
                    }
                }
            }

            if (best < 1000000) {
                prizes++;
                totalTokens += best;
            }

            i += 3;
        }

        Console.WriteLine(totalTokens);
    }

    static int Get(string line, string marker) {
        int i = line.IndexOf(marker);
        if (i == -1) return 0;
        i += marker.Length;
        string number = "";
        while (i < line.Length && char.IsDigit(line[i])) {
            number += line[i];
            i++;
        }
        return int.Parse(number);
    }
}