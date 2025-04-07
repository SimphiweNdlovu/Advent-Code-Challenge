﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
class Program {
    static void Main() {
        var path = "input.txt";
        if (!File.Exists(path)) {
            Console.WriteLine("Missing input file.");
            return;
        }
        string input = File.ReadAllText(path).Trim();
        List<int> disk = new List<int>();
        bool isFile = true;
        int fileID = 0;
        foreach (char c in input) {
            int count = c - '0';
            if (isFile) {
                for (int i = 0; i < count; i++) disk.Add(fileID);
                fileID++;
            } else {
                for (int i = 0; i < count; i++) disk.Add(-1);
            }
            isFile = !isFile;
        }
        while (true) {
            int left = disk.FindIndex(x => x == -1);
            if (left == -1) break;
            int right = -1;
            for (int i = disk.Count - 1; i > left; i--) {
                if (disk[i] != -1) { right = i; break; }
            }
            if (right == -1 || right <= left) break;
            disk[left] = disk[right];
            disk[right] = -1;
        }
        long checksum = 0;
        for (int i = 0; i < disk.Count; i++) {
            if (disk[i] != -1)
                checksum += (long)i * disk[i];
        }
        Console.WriteLine(checksum);
    }
}