using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");
        long totalSum = 0;

        // Process each equation
        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            long testValue = long.Parse(parts[0].Trim()); // Changed to long
            string[] numbers = parts[1].Trim().Split(' ');

            long[] nums = new long[numbers.Length]; // Changed to long
            for (int i = 0; i < numbers.Length; i++)
            {
                nums[i] = long.Parse(numbers[i]); // Changed to long
            }

            // Generate all possible operator combinations
            var results = GenerateOperatorCombinations(nums);

            // Check if any combination matches the test value
            foreach (var result in results)
            {
                if (result == testValue)
                {
                    totalSum += testValue;
                    break;
                }
            }
        }

        Console.WriteLine("Total Calibration Result: " + totalSum);
    }

    // Generate all possible operator combinations between the numbers
    static long[] GenerateOperatorCombinations(long[] nums)
    {
        int n = nums.Length - 1;
        int totalCombinations = (int)Math.Pow(2, n); // 2 choices (add or multiply) for each gap

        long[] results = new long[totalCombinations];
        for (int i = 0; i < totalCombinations; i++)
        {
            long result = nums[0];
            int currentCombination = i;

            // Build the expression for this combination
            for (int j = 0; j < n; j++)
            {
                if ((currentCombination & (1 << j)) != 0) // If bit is 1, use multiplication
                    result *= nums[j + 1];
                else // Otherwise, use addition
                    result += nums[j + 1];
            }
            results[i] = result;
        }

        return results;
    }
}