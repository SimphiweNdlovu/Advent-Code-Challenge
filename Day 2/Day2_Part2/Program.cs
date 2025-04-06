class Program
{
    static void Main()
    {
        var path = "input.txt";

        if (!File.Exists(path))
            return;

        string input = File.ReadAllText(path);
        var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        int safeCount = 0;

        foreach (var line in lines)
        {
            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            IntList levels = new IntList();

            foreach (var part in parts)
            {
                if (int.TryParse(part, out int num))
                    levels.Add(num);
            }

            if (IsSafe(levels))
            {
                safeCount++;
                Console.WriteLine(line);
            }
            else
            {
                // Try removing each element once to see if it becomes safe
            
                for (int i = 0; i < levels.Count; i++)
                {
                    IntList modified = levels.RemoveAt(i);
                    if (IsSafe(modified))
                    {
                        safeCount++;
                        Console.WriteLine(line + " (safe by removing index " + i + ")");
                        
                        break;
                    }
                }
            }
        }

        Console.WriteLine($"Total safe reports: {safeCount}");
    }

    static bool IsSafe(IntList levels)
    {
        if (levels.Count < 2)
            return false;

        bool increasing = levels[1] > levels[0];
        bool decreasing = levels[1] < levels[0];

        for (int i = 1; i < levels.Count; i++)
        {
            int diff = levels[i] - levels[i - 1];

            if (diff == 0 || Math.Abs(diff) > 3)
                return false;

            if (increasing && diff <= 0)
                return false;

            if (decreasing && diff >= 0)
                return false;
        }

        return true;
    }
}