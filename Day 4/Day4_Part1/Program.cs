
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

        int wordCount = 0;
        string word = "XMAS";
        int wordLength = word.Length;

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                wordCount += CountWordInDirections(lines, word, i, j, wordLength);
            }
        }

        Console.WriteLine(wordCount);
    }

   static int CountWordInDirections(string[] grid, string word, int row, int col, int wordLength)
    {
        int count = 0;
        
        int[,] directions = new int[,]
        {
            {0, 1},   // Right
            {0, -1},  // Left
            {1, 0},   // Down
            {-1, 0},  // Up
            {1, 1},   // Down-Right diagonal
            {-1, -1}, // Up-Left diagonal
            {1, -1},  // Down-Left diagonal
            {-1, 1}   // Up-Right diagonal
        };

        // Looping through each direction (each row in directions array)
        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int dx = directions[i, 0];
            int dy = directions[i, 1];
            if (IsWordInDirection(grid, word, row, col, dx, dy, wordLength))
            {
                count++;
            }
        }

        return count;
    }

    static bool IsWordInDirection(string[] grid, string word, int row, int col, int dx, int dy, int wordLength)
    {
        for (int i = 0; i < wordLength; i++)
        {
            int newRow = row + i * dx;
            int newCol = col + i * dy;
            
            if (newRow < 0 || newRow >= grid.Length || newCol < 0 || newCol >= grid[0].Length)
            {
                return false;
            }

            if (grid[newRow][newCol] != word[i])
            {
                return false;
            }
        }
        return true;
    }
}