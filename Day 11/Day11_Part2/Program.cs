using System;
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

        // Use a dictionary to track how many of each stone exist
        Dictionary<string, long> stoneCounts = new Dictionary<string, long>();
        
        // Initialize the dictionary with the input stones
        foreach (var stone in parts) {
            if (stoneCounts.ContainsKey(stone)) {
                stoneCounts[stone]++;
            } else {
                stoneCounts[stone] = 1;
            }
        }

        // Perform 75 blinks
        for (int step = 0; step < 75; step++) {
            // Prepare a new dictionary for the next state
            Dictionary<string, long> nextStoneCounts = new Dictionary<string, long>();

            foreach (var entry in stoneCounts) {
                string stone = entry.Key;
                long count = entry.Value;

                if (stone == "0") {
                    // Rule 1: Replace '0' with '1'
                    if (nextStoneCounts.ContainsKey("1")) {
                        nextStoneCounts["1"] += count;
                    } else {
                        nextStoneCounts["1"] = count;
                    }
                }
                else if (stone.Length % 2 == 0) {
                    // Rule 2: Split the stone with an even number of digits
                    int mid = stone.Length / 2;
                    string left = stone.Substring(0, mid).TrimStart('0');
                    string right = stone.Substring(mid).TrimStart('0');

                    if (string.IsNullOrEmpty(left)) left = "0";
                    if (string.IsNullOrEmpty(right)) right = "0";

                    if (nextStoneCounts.ContainsKey(left)) {
                        nextStoneCounts[left] += count;
                    } else {
                        nextStoneCounts[left] = count;
                    }

                    if (nextStoneCounts.ContainsKey(right)) {
                        nextStoneCounts[right] += count;
                    } else {
                        nextStoneCounts[right] = count;
                    }
                }
                else {
                    // Rule 3: Multiply the stone by 2024
                    long number = long.Parse(stone);
                    string newStone = (number * 2024).ToString();

                    if (nextStoneCounts.ContainsKey(newStone)) {
                        nextStoneCounts[newStone] += count;
                    } else {
                        nextStoneCounts[newStone] = count;
                    }
                }
            }

            // Move to the next state
            stoneCounts = nextStoneCounts;
        }

        // Calculate the total number of stones after 75 blinks
        long totalStones = 0;
        foreach (var entry in stoneCounts) {
            totalStones += entry.Value;
        }

        Console.WriteLine(totalStones);
    }
}
