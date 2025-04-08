using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string path = "input.txt";
        if (!File.Exists(path))
        {
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

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (visited[r, c]) continue;

                char plant = map[r, c];
                List<(int, int)> cells = new List<(int, int)>();
                bool[,] currentRegion = new bool[rows, cols];

                var stack = new Stack<(int, int)>();
                stack.Push((r, c));
                visited[r, c] = true;
                currentRegion[r, c] = true;
                cells.Add((r, c));

                while (stack.Count > 0)
                {
                    var (cr, cc) = stack.Pop();

                    // Explore neighbors
                    // Up
                    if (cr - 1 >= 0 && map[cr - 1, cc] == plant && !visited[cr - 1, cc])
                    {
                        visited[cr - 1, cc] = true;
                        currentRegion[cr - 1, cc] = true;
                        stack.Push((cr - 1, cc));
                        cells.Add((cr - 1, cc));
                    }
                    // Down
                    if (cr + 1 < rows && map[cr + 1, cc] == plant && !visited[cr + 1, cc])
                    {
                        visited[cr + 1, cc] = true;
                        currentRegion[cr + 1, cc] = true;
                        stack.Push((cr + 1, cc));
                        cells.Add((cr + 1, cc));
                    }
                    // Left
                    if (cc - 1 >= 0 && map[cr, cc - 1] == plant && !visited[cr, cc - 1])
                    {
                        visited[cr, cc - 1] = true;
                        currentRegion[cr, cc - 1] = true;
                        stack.Push((cr, cc - 1));
                        cells.Add((cr, cc - 1));
                    }
                    // Right
                    if (cc + 1 < cols && map[cr, cc + 1] == plant && !visited[cr, cc + 1])
                    {
                        visited[cr, cc + 1] = true;
                        currentRegion[cr, cc + 1] = true;
                        stack.Push((cr, cc + 1));
                        cells.Add((cr, cc + 1));
                    }
                }

                // Calculate exposed edges for each cell in the region
                bool[,] topExposed = new bool[rows, cols];
                bool[,] bottomExposed = new bool[rows, cols];
                bool[,] leftExposed = new bool[rows, cols];
                bool[,] rightExposed = new bool[rows, cols];

                foreach (var (cr, cc) in cells)
                {
                    // Top edge
                    if (cr - 1 < 0 || map[cr - 1, cc] != plant)
                        topExposed[cr, cc] = true;

                    // Bottom edge
                    if (cr + 1 >= rows || map[cr + 1, cc] != plant)
                        bottomExposed[cr, cc] = true;

                    // Left edge
                    if (cc - 1 < 0 || map[cr, cc - 1] != plant)
                        leftExposed[cr, cc] = true;

                    // Right edge
                    if (cc + 1 >= cols || map[cr, cc + 1] != plant)
                        rightExposed[cr, cc] = true;
                }

                // Count line segments for each direction
                int topSegments = 0;
                for (int row = 0; row < rows; row++)
                {
                    bool prevTop = false;
                    for (int col = 0; col < cols; col++)
                    {
                        if (currentRegion[row, col] && topExposed[row, col])
                        {
                            if (!prevTop)
                            {
                                topSegments++;
                                prevTop = true;
                            }
                        }
                        else
                        {
                            prevTop = false;
                        }
                    }
                }

                int bottomSegments = 0;
                for (int row = 0; row < rows; row++)
                {
                    bool prevBottom = false;
                    for (int col = 0; col < cols; col++)
                    {
                        if (currentRegion[row, col] && bottomExposed[row, col])
                        {
                            if (!prevBottom)
                            {
                                bottomSegments++;
                                prevBottom = true;
                            }
                        }
                        else
                        {
                            prevBottom = false;
                        }
                    }
                }

                int leftSegments = 0;
                for (int col = 0; col < cols; col++)
                {
                    bool prevLeft = false;
                    for (int row = 0; row < rows; row++)
                    {
                        if (currentRegion[row, col] && leftExposed[row, col])
                        {
                            if (!prevLeft)
                            {
                                leftSegments++;
                                prevLeft = true;
                            }
                        }
                        else
                        {
                            prevLeft = false;
                        }
                    }
                }

                int rightSegments = 0;
                for (int col = 0; col < cols; col++)
                {
                    bool prevRight = false;
                    for (int row = 0; row < rows; row++)
                    {
                        if (currentRegion[row, col] && rightExposed[row, col])
                        {
                            if (!prevRight)
                            {
                                rightSegments++;
                                prevRight = true;
                            }
                        }
                        else
                        {
                            prevRight = false;
                        }
                    }
                }

                int totalSides = topSegments + bottomSegments + leftSegments + rightSegments;
                int area = cells.Count;
                total += area * totalSides;
            }
        }

        Console.WriteLine(total);
    }
}