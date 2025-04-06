using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] map = File.ReadAllLines("input.txt");
        int rows = map.Length;
        int cols = map[0].Length;

        // Store antennas: frequency, x, y
        char[] freqs = new char[1000];
        int[] xs = new int[1000];
        int[] ys = new int[1000];
        int antennaCount = 0;

        for (int y = 0; y < rows; y++)
        {
            string line = map[y];
            for (int x = 0; x < cols; x++)
            {
                char ch = line[x];
                if (ch != '.')
                {
                    freqs[antennaCount] = ch;
                    xs[antennaCount] = x;
                    ys[antennaCount] = y;
                    antennaCount++;
                }
            }
        }

        // Track antinodes
        bool[,] antinodeMap = new bool[rows, cols];
        int antinodeCount = 0;

        for (int i = 0; i < antennaCount; i++)
        {
            for (int j = i + 1; j < antennaCount; j++)
            {
                if (freqs[i] != freqs[j]) continue;

                int x1 = xs[i], y1 = ys[i];
                int x2 = xs[j], y2 = ys[j];

                // Direction vector from A1 to A2
                int dx = x2 - x1;
                int dy = y2 - y1;

                // Compute both antinode positions
                int ax1 = x1 - dx; // 2*A1 - A2
                int ay1 = y1 - dy;
                int ax2 = x2 + dx; // 2*A2 - A1
                int ay2 = y2 + dy;

                // If within bounds and not marked yet
                if (IsInside(ax1, ay1, cols, rows) && !antinodeMap[ay1, ax1])
                {
                    antinodeMap[ay1, ax1] = true;
                    antinodeCount++;
                }

                if (IsInside(ax2, ay2, cols, rows) && !antinodeMap[ay2, ax2])
                {
                    antinodeMap[ay2, ax2] = true;
                    antinodeCount++;
                }
            }
        }

        Console.WriteLine("Total antinode locations: " + antinodeCount);
    }

    static bool IsInside(int x, int y, int cols, int rows)
    {
        return x >= 0 && x < cols && y >= 0 && y < rows;
    }
}
