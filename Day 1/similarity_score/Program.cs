using System;
using System.IO;

class Program
{
    static void Main()
    {
        var path = "input.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine("Missing input file.");
            return;
        }

        var leftList = new IntList();
        var rightList = new IntList();

        try
        {
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    continue;

                if (int.TryParse(parts[0], out int a) && int.TryParse(parts[1], out int b))
                {
                    leftList.Add(a);
                    rightList.Add(b);
                }
            }

            long similarityScore = 0;

            for (int i = 0; i < leftList.Count; i++)
            {
                int value = leftList[i];
                int count = CountOccurrences(rightList, value);
                similarityScore += value * count;
            }

            Console.WriteLine("Similarity score: " + similarityScore);
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong:");
            Console.WriteLine(e.Message);
        }
    }

    // Count how many times a value appears in the list
    static int CountOccurrences(IntList list, int value)
    {
        int count = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == value)
                count++;
        }
        return count;
    }
}