using System;
using System.Collections.Generic;
using System.IO;

class OperatorCalibration
{
    static void Main(string[] args)
    {
        // Read input from file
        string[] input;
        try
        {
            input = File.ReadAllLines("input.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return;
        }

        long totalCalibrationResult = 0;

        foreach (var line in input)
        {
            // Ignore empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Parse test value and numbers
            var parts = line.Split(':');
            if (parts.Length != 2)
            {
                Console.WriteLine($"Invalid input format: {line}");
                continue;
            }

            long testValue;
            if (!long.TryParse(parts[0].Trim(), out testValue))
            {
                Console.WriteLine($"Invalid test value: {parts[0].Trim()}");
                continue;
            }

            var numberStrings = parts[1].Trim().Split(' ');
            long[] numbers = new long[numberStrings.Length];
            for (int i = 0; i < numberStrings.Length; i++)
            {
                if (!long.TryParse(numberStrings[i], out numbers[i]))
                {
                    Console.WriteLine($"Invalid number in input: {numberStrings[i]}");
                    return;
                }
            }

            // Check if equation can be solved
            if (CanSolve(numbers, testValue))
            {
                totalCalibrationResult += testValue;
            }
        }

        Console.WriteLine($"Total Calibration Result: {totalCalibrationResult}");
    }

    static bool CanSolve(long[] numbers, long target)
    {
        // Generate all operator combinations
        int numOperators = numbers.Length - 1;
        var operatorCombinations = GenerateOperatorCombinations(numOperators);

        // Test each combination
        foreach (var operators in operatorCombinations)
        {
            if (EvaluateExpression(numbers, operators) == target)
            {
                return true;
            }
        }

        return false;
    }

    static List<string> GenerateOperatorCombinations(int length)
    {
        var combinations = new List<string>();
        int totalCombinations = (int)Math.Pow(3, length); // 3^length for +, *, ||

        for (int i = 0; i < totalCombinations; i++)
        {
            char[] combination = new char[length];
            int temp = i;
            for (int j = 0; j < length; j++)
            {
                int remainder = temp % 3;
                temp /= 3;
                combination[j] = remainder switch
                {
                    0 => '+',
                    1 => '*',
                    _ => '|', // Representing || operator
                };
            }
            combinations.Add(new string(combination));
        }

        return combinations;
    }

    static long EvaluateExpression(long[] numbers, string operators)
    {
        long result = numbers[0];

        for (int i = 0; i < operators.Length; i++)
        {
            if (operators[i] == '+')
            {
                result += numbers[i + 1];
            }
            else if (operators[i] == '*')
            {
                result *= numbers[i + 1];
            }
            else if (operators[i] == '|') // Handle concatenation
            {
                string concatenated = result.ToString() + numbers[i + 1].ToString();
                result = long.Parse(concatenated);
            }
        }

        return result;
    }
}
