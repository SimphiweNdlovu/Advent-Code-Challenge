using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string[] allLines = File.ReadAllLines("input.txt");

        // Split into grid and movement parts
        List<string> gridLines = new List<string>();
        int i;
        for (i = 0; i < allLines.Length; i++)
        {
            string line = allLines[i].Trim();
            if (string.IsNullOrEmpty(line))
                break;
            gridLines.Add(line);
        }

        string movement = "";
        for (; i < allLines.Length; i++)
            movement += allLines[i].Trim().Replace(" ", "").Replace("\t", "");

        // Parse grid
        int rows = gridLines.Count;
        int cols = gridLines[0].Length;
        int robot_r = -1, robot_c = -1;
        HashSet<Tuple<int, int>> boxes = new HashSet<Tuple<int, int>>();
        HashSet<Tuple<int, int>> walls = new HashSet<Tuple<int, int>>();

        for (int r = 0; r < rows; r++)
        {
            string line = gridLines[r];
            for (int c = 0; c < line.Length; c++)
            {
                char ch = line[c];
                if (ch == '@')
                {
                    robot_r = r;
                    robot_c = c;
                }
                else if (ch == 'O')
                    boxes.Add(Tuple.Create(r, c));
                else if (ch == '#')
                    walls.Add(Tuple.Create(r, c));
            }
        }

        // Process each move
        foreach (char move in movement)
        {
            int dr = 0, dc = 0;
            switch (move)
            {
                case '^': dr = -1; break;
                case 'v': dr = 1; break;
                case '<': dc = -1; break;
                case '>': dc = 1; break;
                default: continue;
            }

            int new_r = robot_r + dr;
            int new_c = robot_c + dc;

            // Check boundaries
            if (new_r < 0 || new_r >= rows || new_c < 0 || new_c >= cols)
                continue;

            // Check wall
            if (walls.Contains(Tuple.Create(new_r, new_c)))
                continue;

            if (boxes.Contains(Tuple.Create(new_r, new_c)))
            {
                int beyond_r = new_r + dr;
                int beyond_c = new_c + dc;

                if (beyond_r < 0 || beyond_r >= rows || beyond_c < 0 || beyond_c >= cols)
                    continue;

                if (walls.Contains(Tuple.Create(beyond_r, beyond_c)) || boxes.Contains(Tuple.Create(beyond_r, beyond_c)))
                    continue;

                boxes.Remove(Tuple.Create(new_r, new_c));
                boxes.Add(Tuple.Create(beyond_r, beyond_c));
                robot_r = new_r;
                robot_c = new_c;
            }
            else
            {
                robot_r = new_r;
                robot_c = new_c;
            }
        }

        // Calculate sum
        int sum = 0;
        foreach (var box in boxes)
            sum += box.Item1 * 100 + box.Item2;

        Console.WriteLine(sum);
    }
}