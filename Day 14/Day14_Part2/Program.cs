using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        List<Robot> robots = new List<Robot>();
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            // Parse position: "p=0,4" -> split into x and y
            var pPart = parts[0].Substring(2).Split(',');
            long x = long.Parse(pPart[0]);
            long y = long.Parse(pPart[1]);
            // Parse velocity: "v=3,-3" -> split into vx and vy
            var vPart = parts[1].Substring(2).Split(',');
            long vx = long.Parse(vPart[0]);
            long vy = long.Parse(vPart[1]);

            robots.Add(new Robot { X = x, Y = y, Vx = vx, Vy = vy });
        }

        int maxT = 101 * 103; // The period after which positions repeat

        long minArea = long.MaxValue;
        int bestT = 0;

        for (int t = 0; t <= maxT; t++)
        {
            List<long> xCoords = new List<long>();
            List<long> yCoords = new List<long>();

            foreach (var robot in robots)
            {
                // Compute positions modulo grid dimensions with adjustments for negatives
                long x = (robot.X + robot.Vx * t) % 101;
                if (x < 0) x += 101;
                long y = (robot.Y + robot.Vy * t) % 103;
                if (y < 0) y += 103;
                xCoords.Add(x);
                yCoords.Add(y);
            }

            xCoords.Sort();
            yCoords.Sort();

            long maxGapX = GetMaxGap(xCoords, 101);
            long maxGapY = GetMaxGap(yCoords, 103);

            long area = (101 - maxGapX) * (103 - maxGapY);

            if (area < minArea)
            {
                minArea = area;
                bestT = t;
            }
        }

        Console.WriteLine(bestT);
    }

    static long GetMaxGap(List<long> sortedCoords, long gridSize)
    {
        if (sortedCoords.Count == 0)
            return gridSize; // All tiles are a gap if no robots

        long maxGap = 0;
        for (int i = 1; i < sortedCoords.Count; i++)
            maxGap = Math.Max(maxGap, sortedCoords[i] - sortedCoords[i - 1]);

        // Check wrap-around gap
        maxGap = Math.Max(maxGap, sortedCoords[0] + gridSize - sortedCoords.Last());
        return maxGap;
    }
}

class Robot
{
    public long X { get; set; }
    public long Y { get; set; }
    public long Vx { get; set; }
    public long Vy { get; set; }
}