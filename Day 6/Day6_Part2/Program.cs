using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var path = "input.txt";
        if (!File.Exists(path)) return;

        string[] map = File.ReadAllLines(path);
        int rows = map.Length;
        int cols = map[0].Length;

        // Directions (up, right, down, left)
        int[,] directions = { {-1, 0}, {0, 1}, {1, 0}, {0, -1} };
        int currentDirection = 0; // Initially the guard is facing up (^)

        // Find the initial position of the guard
        int startRow = -1, startCol = -1;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (map[r][c] == '^' || map[r][c] == 'v' || map[r][c] == '>' || map[r][c] == '<')
                {
                    startRow = r;
                    startCol = c;
                    if (map[r][c] == '^') currentDirection = 0;
                    else if (map[r][c] == '>') currentDirection = 1;
                    else if (map[r][c] == 'v') currentDirection = 2;
                    else if (map[r][c] == '<') currentDirection = 3;
                    break;
                }
            }
            if (startRow != -1) break;
        }

        // Call the method to count the possible positions for the new obstruction
        int result = CountLoopPositions(map, rows, cols, startRow, startCol, currentDirection, directions);

        // Output the result
        Console.WriteLine(result);
    }

    // Function to simulate the guard's movement and detect loops with a new obstruction
    static int CountLoopPositions(string[] map, int rows, int cols, int startRow, int startCol, int currentDirection, int[,] directions)
    {
        HashSet<(int, int, int)> visited = new HashSet<(int, int, int)>();
        List<(int, int)> possiblePositions = new List<(int, int)>();

        // Try placing a new obstruction in each free space (.)
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (map[r][c] == '.' && (r != startRow || c != startCol))
                {
                    // Simulate with a new obstruction at (r, c)
                    string[] modifiedMap = (string[])map.Clone();
                    modifiedMap[r] = modifiedMap[r].Substring(0, c) + '#' + modifiedMap[r].Substring(c + 1);

                    int row = startRow, col = startCol, dir = currentDirection;
                    bool isLoop = false;

                    // Track visited positions in the format (row, col, direction)
                    while (true)
                    {
                        if (row < 0 || row >= rows || col < 0 || col >= cols)
                            break;

                        if (modifiedMap[row][col] == '#')
                            break;

                        if (visited.Contains((row, col, dir))) // Loop detected
                        {
                            isLoop = true;
                            break;
                        }

                        visited.Add((row, col, dir));

                        // Move forward or turn
                        int newRow = row + directions[dir, 0];
                        int newCol = col + directions[dir, 1];

                        if (modifiedMap[newRow][newCol] == '#')
                        {
                            dir = (dir + 1) % 4; // Turn right (clockwise)
                        }
                        else
                        {
                            row = newRow;
                            col = newCol;
                        }
                    }

                    if (isLoop)
                        possiblePositions.Add((r, c));
                }
            }
        }

        return possiblePositions.Count;
    }
}
