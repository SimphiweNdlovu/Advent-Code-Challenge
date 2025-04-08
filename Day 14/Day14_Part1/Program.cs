using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        int q1 = 0, q2 = 0, q3 = 0, q4 = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            // Parse position
            var pPart = parts[0].Substring(2).Split(',');
            long x = long.Parse(pPart[0]);
            long y = long.Parse(pPart[1]);
            // Parse velocity
            var vPart = parts[1].Substring(2).Split(',');
            long vx = long.Parse(vPart[0]);
            long vy = long.Parse(vPart[1]);

            // Compute new position after 100 seconds with wrapping
            long newX = (x + vx * 100) % 101;
            if (newX < 0)
                newX += 101;

            long newY = (y + vy * 100) % 103;
            if (newY < 0)
                newY += 103;

            // Check if on middle lines
            if (newX == 50 || newY == 51)
                continue;

            // Determine quadrant
            if (newX < 50 && newY < 51)
                q1++;
            else if (newX > 50 && newY < 51)
                q2++;
            else if (newX < 50 && newY > 51)
                q3++;
            else
                q4++;
        }

        long safetyFactor = (long)q1 * q2 * q3 * q4;
        Console.WriteLine(safetyFactor);
    }
}