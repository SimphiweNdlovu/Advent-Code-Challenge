using System;
using System.Collections.Generic;

class Prize
{
    public long X { get; set; }
    public long Y { get; set; }
    public bool IsReachable { get; set; }
    public int MinPresses { get; set; }
}

class Button
{
    public long DX { get; set; }
    public long DY { get; set; }
}

class Program
{
    static void Main()
    {
        // Sample input - in reality, you would parse this from the actual input
        var prizes = new List<Prize>
        {
            new Prize { X = 3288, Y = 1772 },
            new Prize { X = 12452, Y = 2246 },
            new Prize { X = 10554, Y = 7484 },
            new Prize { X = 5960, Y = 3140 },
            new Prize { X = 15646, Y = 8526 },
            // Add all other prizes here...
        };

        var buttons = new List<Button[]>
        {
            new Button[] { new Button { DX = 57, DY = 16 }, new Button { DX = 20, DY = 74 } },
            new Button[] { new Button { DX = 60, DY = 17 }, new Button { DX = 18, DY = 40 } },
            new Button[] { new Button { DX = 53, DY = 30 }, new Button { DX = 12, DY = 48 } },
            new Button[] { new Button { DX = 94, DY = 41 }, new Button { DX = 16, DY = 34 } },
            new Button[] { new Button { DX = 51, DY = 15 }, new Button { DX = 16, DY = 53 } },
            // Add all other button pairs here...
        };

        foreach (var prize in prizes)
        {
            prize.IsReachable = false;
            prize.MinPresses = int.MaxValue;

            // We'll use BFS to find the minimal presses for each prize
            var visited = new HashSet<(long, long)>();
            var queue = new Queue<(long x, long y, int presses)>();

            queue.Enqueue((0, 0, 0));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.x == prize.X && current.y == prize.Y)
                {
                    prize.IsReachable = true;
                    prize.MinPresses = current.presses;
                    break;
                }

                if (current.presses > 100) // Practical limit to prevent infinite loops
                    continue;

                foreach (var buttonPair in buttons)
                {
                    foreach (var button in buttonPair)
                    {
                        long newX = current.x + button.DX;
                        long newY = current.y + button.DY;
                        if (!visited.Contains((newX, newY)))
                        {
                            visited.Add((newX, newY));
                            queue.Enqueue((newX, newY, current.presses + 1));
                        }
                    }
                }
            }
        }

        // Calculate total minimal tokens (presses) for all reachable prizes
        int totalTokens = 0;
        int reachableCount = 0;
        foreach (var prize in prizes)
        {
            if (prize.IsReachable)
            {
                totalTokens += prize.MinPresses;
                reachableCount++;
            }
        }

        Console.WriteLine($"Reachable prizes: {reachableCount}");
        Console.WriteLine($"Total tokens needed: {totalTokens}");
    }
}