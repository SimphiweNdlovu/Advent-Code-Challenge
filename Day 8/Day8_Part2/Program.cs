using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static int GCD(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    
    static void Main()
    {
        var path = "input.txt";
        if (!File.Exists(path))
        {
            Console.WriteLine("Missing input file.");
            return;
        }
        
        string[] lines = File.ReadAllLines(path);
        int height = lines.Length;
        if (height == 0) return;
        
        // Find the maximum width in the input
        int width = 0;
        foreach (var line in lines)
        {
            if (line.Length > width) width = line.Length;
        }
        
        var antennas = new Dictionary<char, List<(int r, int c)>>();
        for (int r = 0; r < height; r++)
        {
            for (int c = 0; c < lines[r].Length; c++)  // Use lines[r].Length to handle varying row lengths
            {
                char ch = lines[r][c];
                if (ch == '.') continue;
                if (!antennas.ContainsKey(ch))
                    antennas[ch] = new List<(int, int)>();
                antennas[ch].Add((r, c));
            }
        }
        
        var antinodes = new HashSet<(int, int)>();
        foreach (var kv in antennas)
        {
            List<(int r, int c)> list = kv.Value;
            if (list.Count < 2)
                continue;
            
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    var p1 = list[i];
                    var p2 = list[j];
                    int dr = p2.r - p1.r;
                    int dc = p2.c - p1.c;
                    int d = GCD(dr, dc);
                    int stepR = dr / d;
                    int stepC = dc / d;
                    int startR = p1.r, startC = p1.c;
                    while (true)
                    {
                        int prevR = startR - stepR;
                        int prevC = startC - stepC;
                        if (prevR < 0 || prevR >= height || prevC < 0 || prevC >= width)
                            break;
                        startR = prevR;
                        startC = prevC;
                    }
                    int rPos = startR, cPos = startC;
                    while (rPos >= 0 && rPos < height && cPos >= 0 && cPos < width)
                    {
                        antinodes.Add((rPos, cPos));
                        rPos += stepR;
                        cPos += stepC;
                    }
                }
            }
        }
        
        Console.WriteLine(antinodes.Count);
    }
}
