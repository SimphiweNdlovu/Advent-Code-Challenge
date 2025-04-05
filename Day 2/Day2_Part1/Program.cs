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
        int safeCount = 0;

        foreach (var line in lines)
        {
            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            IntList levels = new IntList();

            foreach (var part in parts)
            {
                if (int.TryParse(part, out int num))
                {
                    levels.Add(num);
                }
            }

            if (levels.Count < 2)
                continue;

            bool increasing = levels[1] > levels[0];
            bool decreasing = levels[1] < levels[0];
            bool valid = true;

            for (int i = 1; i < levels.Count; i++)
            {
                int diff = levels[i] - levels[i - 1];

                if (diff == 0 || Math.Abs(diff) > 3)
                {
                    valid = false;
                    break;
                }

                if (increasing && diff <= 0)
                {
                    valid = false;
                    break;
                }

                if (decreasing && diff >= 0)
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                safeCount++;
                Console.WriteLine(line);
            }
        }

        Console.WriteLine($"{safeCount}");
    }
}