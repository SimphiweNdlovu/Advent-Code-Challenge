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
        int currentDirection = 0; // Initially the guard is facing up (^

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
                    if (map[r][c] == '^') 
                        currentDirection = 0;
                    else if (map[r][c] == '>') 
                        currentDirection = 1;
                    else if (map[r][c] == 'v') 
                        currentDirection = 2;
                    else if (map[r][c] == '<')
                         currentDirection = 3;
                    break;
                }
            }
            if (startRow != -1) break; // Break outer loop if guard is found


        }

         // Set to track visited positions
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        int row = startRow, col = startCol;
        visited.Add((row, col));

        while (true)
        {
            // Move in the current direction
            int newRow = row + directions[currentDirection, 0];
            int newCol = col + directions[currentDirection, 1];

            // Check if the guard goes out of bounds
            if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
                break;

            // Check the next cell
            if (map[newRow][newCol] == '#')  // Obstacle encountered, turn right
            {
                currentDirection = (currentDirection + 1) % 4;
            }
            else  // Move forward
            {
                row = newRow;
                col = newCol;
                visited.Add((row, col));
            }
        }

        // Output the number of distinct positions visited
        Console.WriteLine("number of distinct positions visited: " +visited.Count);
    }

}