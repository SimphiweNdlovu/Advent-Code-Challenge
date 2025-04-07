﻿using System;
using System.IO;

class Program {
    static void Main() {
        string path = "input.txt";
        if (!File.Exists(path)) {
            Console.WriteLine("Missing input file.");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        int rows = lines.Length;
        int cols = lines[0].Length;

        char[,] map = new char[rows, cols];
        bool[,] visited = new bool[rows, cols];

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                map[r, c] = lines[r][c];

        int total = 0;

        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                if (visited[r, c]) continue;

                char plant = map[r, c];
                int area = 0;
                int perimeter = 0;

                int[] stackRow = new int[rows * cols];
                int[] stackCol = new int[rows * cols];
                int top = 0;

                stackRow[top] = r;
                stackCol[top] = c;
                top++;
                visited[r, c] = true;

                while (top > 0) {
                    top--;
                    int cr = stackRow[top];
                    int cc = stackCol[top];

                    area++;
                    int sides = 0;

                    if (cr - 1 < 0 || map[cr - 1, cc] != plant) sides++;
                    else if (!visited[cr - 1, cc]) {
                        visited[cr - 1, cc] = true;
                        stackRow[top] = cr - 1;
                        stackCol[top] = cc;
                        top++;
                    }

                    if (cr + 1 >= rows || map[cr + 1, cc] != plant) sides++;
                    else if (!visited[cr + 1, cc]) {
                        visited[cr + 1, cc] = true;
                        stackRow[top] = cr + 1;
                        stackCol[top] = cc;
                        top++;
                    }

                    if (cc - 1 < 0 || map[cr, cc - 1] != plant) sides++;
                    else if (!visited[cr, cc - 1]) {
                        visited[cr, cc - 1] = true;
                        stackRow[top] = cr;
                        stackCol[top] = cc - 1;
                        top++;
                    }

                    if (cc + 1 >= cols || map[cr, cc + 1] != plant) sides++;
                    else if (!visited[cr, cc + 1]) {
                        visited[cr, cc + 1] = true;
                        stackRow[top] = cr;
                        stackCol[top] = cc + 1;
                        top++;
                    }

                    perimeter += sides;
                }

                total += area * perimeter;
            }
        }

        Console.WriteLine(total);
    }
}