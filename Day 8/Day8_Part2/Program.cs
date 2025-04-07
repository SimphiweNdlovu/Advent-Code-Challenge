using System;
using System.IO;
class Program
{
    struct Antenna
    {
        public int X, Y;
    }

    static void Main()
    {
        string[] map = File.ReadAllLines("input.txt");
        int rows = map.Length;
        int cols = map[0].Length;

        // Store antennas by frequency
        Antenna[][] freqAntennas = new Antenna[128][];
        int[] counts = new int[128];

        for (int i = 0; i < freqAntennas.Length; i++)
            freqAntennas[i] = new Antenna[100];

        for (int y = 0; y < rows; y++)
        {
            string line = map[y];
            for (int x = 0; x < cols; x++)
            {
                char ch = line[x];
                if (ch != '.')
                {
                    freqAntennas[ch][counts[ch]++] = new Antenna { X = x, Y = y };
                }
            }
        }

        bool[,] antinodeMap = new bool[rows, cols];

        for (int f = 0; f < 128; f++)
        {
            int count = counts[f];
            if (count < 2) continue;

            Antenna[] ants = freqAntennas[f];

            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    int dx = ants[j].X - ants[i].X;
                    int dy = ants[j].Y - ants[i].Y;
                    Reduce(ref dx, ref dy);

                    // Scan line in both directions
                    MarkLine(ants[i].X, ants[i].Y, dx, dy, rows, cols, antinodeMap);
                    MarkLine(ants[i].X, ants[i].Y, -dx, -dy, rows, cols, antinodeMap);
                }
            }
        }

        int antinodeCount = 0;
        for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
                if (antinodeMap[y, x]) antinodeCount++;

        Console.WriteLine("Total Part Two antinodes: " + antinodeCount);
    }

    static void Reduce(ref int dx, ref int dy)
    {
        if (dx == 0 && dy == 0) return;
        int g = GCD(Math.Abs(dx), Math.Abs(dy));
        dx /= g;
        dy /= g;
    }

    static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int tmp = b;
            b = a % b;
            a = tmp;
        }
        return a;
    }

    static void MarkLine(int x, int y, int dx, int dy, int rows, int cols, bool[,] map)
    {
        while (x >= 0 && x < cols && y >= 0 && y < rows)
        {
            if (!map[y, x])
                map[y, x] = true;
            x += dx;
            y += dy;
        }
    }
}
