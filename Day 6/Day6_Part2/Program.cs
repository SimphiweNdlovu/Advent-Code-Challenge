using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var path = "input.txt";
        if (!File.Exists(path)) return;

        string[] mapLines = File.ReadAllLines(path);
        int rows = mapLines.Length;
        int cols = mapLines[0].Length;
        char[,] map = new char[rows, cols];

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                map[r, c] = mapLines[r][c];

        // Directions: up, right, down, left
        int[,] directions = { {-1, 0}, {0, 1}, {1, 0}, {0, -1} };

        int startRow = -1, startCol = -1, startDir = 0;

        // Find guard start
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                char ch = map[r, c];
                if (ch == '^' || ch == 'v' || ch == '<' || ch == '>')
                {
                    startRow = r;
                    startCol = c;
                    startDir = ch == '^' ? 0 : ch == '>' ? 1 : ch == 'v' ? 2 : 3;
                    break;
                }
            }
            if (startRow != -1) break;
        }

        int loopPositions = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (map[r, c] != '.') continue;
                if (r == startRow && c == startCol) continue;

                // Temporarily add obstacle
                map[r, c] = '#';

                if (WouldEnterLoop(map, startRow, startCol, startDir, directions))
                    loopPositions++;

                // Revert
                map[r, c] = '.';
            }
        }

        Console.WriteLine("Number of positions that cause a loop: " + loopPositions);
    }

    static bool WouldEnterLoop(char[,] map, int startRow, int startCol, int startDir, int[,] directions)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        HashSet<(int, int, int)> visitedStates = new HashSet<(int, int, int)>();

        int row = startRow, col = startCol, dir = startDir;

        while (true)
        {
            var state = (row, col, dir);
            if (visitedStates.Contains(state))
                return true;

            visitedStates.Add(state);

            int newRow = row + directions[dir, 0];
            int newCol = col + directions[dir, 1];

            if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
                return false;

            if (map[newRow, newCol] == '#')
                dir = (dir + 1) % 4; // turn right
            else
            {
                row = newRow;
                col = newCol;
            }
        }
    }
}
