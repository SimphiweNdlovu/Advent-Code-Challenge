using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");
        int a = 0, b = 0, c = 0;
        List<int> program = new List<int>();

        foreach (string line in lines)
        {
            if (line.StartsWith("Register A: "))
                a = int.Parse(line.Substring("Register A: ".Length));
            else if (line.StartsWith("Register B: "))
                b = int.Parse(line.Substring("Register B: ".Length));
            else if (line.StartsWith("Register C: "))
                c = int.Parse(line.Substring("Register C: ".Length));
            else if (line.StartsWith("Program: "))
            {
                string[] parts = line.Substring("Program: ".Length).Split(',');
                foreach (string part in parts)
                    program.Add(int.Parse(part.Trim()));
            }
        }

        List<string> outputs = new List<string>();
        int ip = 0;

        while (ip < program.Count)
        {
            int opcode = program[ip];
            if (ip + 1 >= program.Count)
                break;

            int operand = program[ip + 1];

            switch (opcode)
            {
                case 0: // adv
                    int comboValue0 = GetComboValue(operand, a, b, c);
                    int denominator0 = (int)Math.Pow(2, comboValue0);
                    a /= denominator0;
                    ip += 2;
                    break;
                case 1: // bxl
                    b ^= operand;
                    ip += 2;
                    break;
                case 2: // bst
                    int comboValue2 = GetComboValue(operand, a, b, c);
                    b = comboValue2 % 8;
                    ip += 2;
                    break;
                case 3: // jnz
                    if (a != 0)
                        ip = operand;
                    else
                        ip += 2;
                    break;
                case 4: // bxc
                    b ^= c;
                    ip += 2;
                    break;
                case 5: // out
                    int comboValue5 = GetComboValue(operand, a, b, c);
                    outputs.Add((comboValue5 % 8).ToString());
                    ip += 2;
                    break;
                case 6: // bdv
                    int comboValue6 = GetComboValue(operand, a, b, c);
                    int denominator6 = (int)Math.Pow(2, comboValue6);
                    b = a / denominator6;
                    ip += 2;
                    break;
                case 7: // cdv
                    int comboValue7 = GetComboValue(operand, a, b, c);
                    int denominator7 = (int)Math.Pow(2, comboValue7);
                    c = a / denominator7;
                    ip += 2;
                    break;
                default:
                    ip = program.Count;
                    break;
            }
        }

        Console.WriteLine(string.Join(",", outputs));
    }

    static int GetComboValue(int operand, int a, int b, int c)
    {
        if (operand <= 3)
            return operand;
        else if (operand == 4)
            return a;
        else if (operand == 5)
            return b;
        else if (operand == 6)
            return c;
        else
            throw new ArgumentException("Invalid combo operand.");
    }
}