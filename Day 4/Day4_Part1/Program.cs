
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

        return count;
    }

    static bool IsWordInDirection(string[] grid, string word, int row, int col, int dx, int dy, int wordLength)
    {
        
        return true;
    }
}