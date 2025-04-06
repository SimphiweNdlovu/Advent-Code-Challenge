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
            long testValue = long.Parse(parts[0].Trim()); // Test value
            string[] numbers = parts[1].Trim().Split(' '); // List of numbers

            long[] nums = new long[numbers.Length]; // Array of numbers
            for (int i = 0; i < numbers.Length; i++)
            {
                nums[i] = long.Parse(numbers[i]); // Parsing numbers
            }

            // Generate all possible operator combinations
            var results = GenerateOperatorCombinations(nums);

            // Check if any combination matches the test value
            foreach (var result in results)
            {
                if (result == testValue)
                {
                    totalSum += testValue; // If valid, add the test value to the total
                    break;
                }
            }
        }

        Console.WriteLine("Total Calibration Result: " + totalSum); // Output the final sum
    }
    // Generate all possible operator combinations between the numbers
    static long[] GenerateOperatorCombinations(long[] nums)
    {
        int n = nums.Length - 1; // Number of operator slots
        int totalCombinations = (int)Math.Pow(3, n); // 3 choices for each gap: +, *, ||

        long[] results = new long[totalCombinations];
        for (int i = 0; i < totalCombinations; i++)
        {
            long result = nums[0];
            int currentCombination = i;

            // Build the expression for this combination
            for (int j = 0; j < n; j++)
            {
                int operatorIndex = currentCombination % 3; // Get the operator for the current slot
                currentCombination /= 3; // Move to the next operator choice

                if (operatorIndex == 0) // Addition
                    result += nums[j + 1];
                else if (operatorIndex == 1) // Multiplication
                    result *= nums[j + 1];
                else if (operatorIndex == 2) // Concatenation
                    result = long.Parse(result.ToString() + nums[j + 1].ToString()); // Concatenate the numbers
            }
            results[i] = result;
        }

        return results;
    }
}