using System;
using System.IO;

class Program
{
    class FileEntry
    {
        public int ID;
        public int Start;
        public int Length;
    }

    static void Main()
    {
        string input = File.ReadAllText("input.txt").Trim();
        var blocks = ParseDisk(input);
        var fileEntries = GetFiles(blocks);

        // Sort files by descending ID
        Array.Sort(fileEntries, (a, b) => b.ID.CompareTo(a.ID));

        foreach (var file in fileEntries)
        {
            int fileLen = file.Length;
            int originalStart = file.Start;

            // Search from left to before the original position
            for (int i = 0; i <= originalStart - fileLen; i++)
            {
                bool fits = true;
                for (int j = 0; j < fileLen; j++)
                {
                    if (blocks[i + j] != -1)
                    {
                        fits = false;
                        break;
                    }
                }

                if (fits)
                {
                    // Clear original file location
                    for (int j = 0; j < fileLen; j++)
                        blocks[originalStart + j] = -1;

                    // Move to new location
                    for (int j = 0; j < fileLen; j++)
                        blocks[i + j] = file.ID;

                    file.Start = i;
                    break;
                }
            }
        }

        long checksum = 0;
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i] != -1)
                checksum += (long)blocks[i] * i;
        }

        Console.WriteLine($"Checksum: {checksum}");
    }

    static int[] ParseDisk(string map)
    {
        var result = new System.Collections.Generic.List<int>();
        bool isFile = true;
        int fileId = 0;

        for (int i = 0; i < map.Length; i++)
        {
            int len = map[i] - '0';
            for (int j = 0; j < len; j++)
            {
                result.Add(isFile ? fileId : -1);
            }

            if (isFile)
                fileId++;

            isFile = !isFile;
        }

        return result.ToArray();
    }

    static FileEntry[] GetFiles(int[] blocks)
    {
        var files = new System.Collections.Generic.List<FileEntry>();
        int i = 0;
        while (i < blocks.Length)
        {
            if (blocks[i] != -1)
            {
                int id = blocks[i];
                int start = i;
                int len = 1;
                i++;

                while (i < blocks.Length && blocks[i] == id)
                {
                    len++;
                    i++;
                }

                files.Add(new FileEntry { ID = id, Start = start, Length = len });
            }
            else
            {
                i++;
            }
        }

        return files.ToArray();
    }
}
