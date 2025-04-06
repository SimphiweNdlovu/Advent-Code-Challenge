using System;
using System.IO;
class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        RuleList rules = new RuleList();
        int lineIndex = 0;

        // Parse rules
        while (lineIndex < lines.Length && lines[lineIndex].Trim().Length > 0)
        {
            string line = lines[lineIndex];
            int pipeIndex = line.IndexOf('|');
            int before = int.Parse(line.Substring(0, pipeIndex));
            int after = int.Parse(line.Substring(pipeIndex + 1));
            rules.Add(before, after);
            lineIndex++;
        }

         lineIndex++; // skip blank line

        IntList middlePages = new IntList();

        // Process updates
        while (lineIndex < lines.Length)
        {
            string[] parts = lines[lineIndex].Split(',');
            int[] update = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                update[i] = int.Parse(parts[i]);

            int[] indexMapKeys = new int[update.Length];
            int[] indexMapValues = new int[update.Length];
            int mapSize = update.Length;
            for (int i = 0; i < update.Length; i++)
            {
                indexMapKeys[i] = update[i];
                indexMapValues[i] = i;
            }

            bool valid = true;
            for (int r = 0; r < rules.Count; r++)
            {
                int before = rules[r].Before;
                int after = rules[r].After;

                int idxBefore = -1, idxAfter = -1;
                for (int i = 0; i < mapSize; i++)
                {
                    if (indexMapKeys[i] == before) idxBefore = indexMapValues[i];
                    if (indexMapKeys[i] == after) idxAfter = indexMapValues[i];
                }

                if (idxBefore != -1 && idxAfter != -1 && idxBefore >= idxAfter)
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                int mid = update[update.Length / 2];
                middlePages.Add(mid);
            }

            lineIndex++;
        }

        Console.WriteLine("Sum of middle page numbers: " + middlePages.Sum());
    }
}
