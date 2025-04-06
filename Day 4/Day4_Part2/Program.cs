
using System;
using System.IO;

class Program
{
    static void Main()
    {
        var path = "input.txt";

        if (!File.Exists(path))
        {
            return;
        }

        string input = File.ReadAllText(path);
        var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

         int rows = grid.Length;
        int cols = grid[0].Length;

          for (int row = 1; row < rows - 1; row++)
        {
            for (int col = 1; col < cols - 1; col++)
            {
                // Check for 'A' in the center
                if (grid[row][col] != 'A') continue;

                // Diagonal characters
                char topLeft = grid[row - 1][col - 1];
                char topRight = grid[row - 1][col + 1];
                char bottomLeft = grid[row + 1][col - 1];
                char bottomRight = grid[row + 1][col + 1];

                // Check if both diagonals form MAS or SAM
                if (IsMASPair(topLeft, bottomRight) && IsMASPair(topRight, bottomLeft))
                {
                    count++;
                }
            }
        }

        Console.WriteLine(count);
    }
       static bool IsMASPair(char first, char second)
       {
        return true;
       }

   
}