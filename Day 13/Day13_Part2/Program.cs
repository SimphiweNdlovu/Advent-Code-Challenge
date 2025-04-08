using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        long totalCost = 0;
        int prizeCount = 0;

        for (int i = 0; i < lines.Length; i += 4)
        {
            // Parse A and B button moves
            var aParts = lines[i].Split(new[] { "X+", ", Y+" }, StringSplitOptions.RemoveEmptyEntries);
            var bParts = lines[i + 1].Split(new[] { "X+", ", Y+" }, StringSplitOptions.RemoveEmptyEntries);
            var pParts = lines[i + 2].Split(new[] { "X=", ", Y=" }, StringSplitOptions.RemoveEmptyEntries);

            long ax = long.Parse(aParts[1]);
            long ay = long.Parse(aParts[2]);
            long bx = long.Parse(bParts[1]);
            long by = long.Parse(bParts[2]);
            long px = long.Parse(pParts[1]) + 10000000000000;
            long py = long.Parse(pParts[2]) + 10000000000000;

            long cx = px;
            long cy = py;

            long D = ax * by - ay * bx;

            bool solvable = false;
            long minCost = long.MaxValue;

            if (D != 0)
            {
                long a_num = by * cx - bx * cy;
                long b_num = ax * cy - ay * cx;

                if (a_num % D != 0 || b_num % D != 0)
                {
                    continue;
                }

                long a = a_num / D;
                long b = b_num / D;

                if (a >= 0 && b >= 0)
                {
                    solvable = true;
                    minCost = 3 * a + b;
                }
            }
            else
            {
                // Check if the system is consistent
                if (ax * cy != ay * cx || bx * cy != by * cx)
                {
                    continue;
                }

                // Solve Ax * a + Bx * b = Cx
                long g = GCD(ax, bx);
                if (cx % g != 0)
                {
                    continue;
                }

                (long gcd, long x, long y) = ExtendedEuclidean(ax, bx);
                long factor = cx / g;

                long a0 = x * factor;
                long b0 = y * factor;

                long stepA = bx / g;
                long stepB = ax / g;

                // Compute kMin and kMax
                long kMin = Ceiling(-a0, stepA);
                long kMax = b0 / stepB;

                if (kMin > kMax)
                {
                    continue;
                }

                // Determine the optimal k
                long coeff = 3 * stepA - stepB;

                long bestK;
                if (coeff > 0)
                {
                    bestK = kMin;
                }
                else if (coeff < 0)
                {
                    bestK = kMax;
                }
                else
                {
                    bestK = kMin; // All k in range give same cost
                }

                long a_kMin = a0 + bestK * stepA;
                long b_kMin = b0 - bestK * stepB;

                long a_kMax = a0 + kMax * stepA;
                long b_kMax = b0 - kMax * stepB;

                // Ensure the calculated a and b are non-negative
                if (a_kMin >= 0 && b_kMin >= 0)
                {
                    minCost = 3 * a_kMin + b_kMin;
                    solvable = true;
                }
                else if (a_kMax >= 0 && b_kMax >= 0)
                {
                    minCost = 3 * a_kMax + b_kMax;
                    solvable = true;
                }
            }

            if (solvable)
            {
                totalCost += minCost;
                prizeCount++;
            }
        }

        Console.WriteLine("Total Prizes Won: " + prizeCount);
        Console.WriteLine("Total Minimum Token Cost: " + totalCost);
    }

    static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static (long gcd, long x, long y) ExtendedEuclidean(long a, long b)
    {
        if (b == 0)
            return (a, 1, 0);
        var (g, x1, y1) = ExtendedEuclidean(b, a % b);
        long x = y1;
        long y = x1 - (a / b) * y1;
        return (g, x, y);
    }

    static long Ceiling(long numerator, long denominator)
    {
        return (numerator + denominator - 1) / denominator;
    }
}