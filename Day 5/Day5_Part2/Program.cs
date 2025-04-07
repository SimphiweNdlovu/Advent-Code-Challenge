using System;
using System.IO;
using System.Collections.Generic;


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

        IntList validMiddles = new IntList();
        IntList correctedMiddles = new IntList();

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
                validMiddles.Add(mid);
            }
            else
            {
                int[] sorted = TopoSort(update, rules);
                int mid = sorted[sorted.Length / 2];
                correctedMiddles.Add(mid);
            }

            lineIndex++;
        }

        Console.WriteLine("Part One sum: " + validMiddles.Sum());
        Console.WriteLine("Part Two sum: " + correctedMiddles.Sum());
    }

    static int[] TopoSort(int[] pages, RuleList rules)
    {
        int n = pages.Length;
        int[] result = new int[n];
        int resultSize = 0;

        Dictionary<int, int> indexMap = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
            indexMap[pages[i]] = i;

        int[,] adj = new int[n, n];
        int[] indegree = new int[n];

        for (int r = 0; r < rules.Count; r++)
        {
            int before = rules[r].Before;
            int after = rules[r].After;

            if (indexMap.ContainsKey(before) && indexMap.ContainsKey(after))
            {
                int u = indexMap[before];
                int v = indexMap[after];
                if (adj[u, v] == 0)
                {
                    adj[u, v] = 1;
                    indegree[v]++;
                }
            }
        }

        bool[] visited = new bool[n];
        for (int step = 0; step < n; step++)
        {
            bool found = false;
            for (int j = 0; j < n; j++)
            {
                if (!visited[j] && indegree[j] == 0)
                {
                    visited[j] = true;
                    result[resultSize++] = pages[j];
                    for (int k = 0; k < n; k++)
                    {
                        if (adj[j, k] == 1)
                            indegree[k]--;
                    }
                    found = true;
                    break;
                }
            }

            if (!found)
                break; // cycle detected, should not happen
        }

        return result;
    }
}
