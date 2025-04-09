using System;
using System.IO;

class State
{
    public int X;
    public int Y;
    public int Dir;
    public int Cost;
}

class Program
{
    static void Main()
    {
        string[] grid = File.ReadAllLines("input.txt");
        int rows = grid.Length;
        int cols = grid[0].Length;

        int startX = -1, startY = -1;
        int endX = -1, endY = -1;

        // Locate the start and end positions
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == 'S')
                {
                    startX = i;
                    startY = j;
                }
                else if (grid[i][j] == 'E')
                {
                    endX = i;
                    endY = j;
                }
            }
        }

        if (startX == -1 || endX == -1)
        {
            Console.WriteLine("Start or end position not found.");
            return;
        }

        // Directions: north (0), east (1), south (2), west (3)
        (int dx, int dy)[] directions = new (int, int)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };

        // Initialize distance array with maximum values
        int[,,] dist = new int[rows, cols, 4];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                for (int d = 0; d < 4; d++)
                    dist[i, j, d] = int.MaxValue;

        // Start facing east (direction 1)
        dist[startX, startY, 1] = 0;

        State[] states = new State[16]; // Initial capacity
        int stateCount = 0;
        states[stateCount++] = new State { X = startX, Y = startY, Dir = 1, Cost = 0 };

        while (stateCount > 0)
        {
            // Find the state with the minimum cost
            int minIndex = 0;
            int minCost = states[0].Cost;
            for (int i = 1; i < stateCount; i++)
            {
                if (states[i].Cost < minCost)
                {
                    minCost = states[i].Cost;
                    minIndex = i;
                }
            }

            State current = states[minIndex];
            // Remove by swapping with the last element
            states[minIndex] = states[stateCount - 1];
            stateCount--;

            // Check if current position is the end
            if (current.X == endX && current.Y == endY)
            {
                Console.WriteLine(current.Cost);
                return;
            }

            // Skip if a better path has already been found
            if (current.Cost > dist[current.X, current.Y, current.Dir])
                continue;

            // Move forward
            (int dx, int dy) = directions[current.Dir];
            int nx = current.X + dx;
            int ny = current.Y + dy;

            if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && grid[nx][ny] != '#')
            {
                int newCost = current.Cost + 1;
                if (newCost < dist[nx, ny, current.Dir])
                {
                    dist[nx, ny, current.Dir] = newCost;
                    // Ensure the array has enough space
                    if (stateCount == states.Length)
                        Array.Resize(ref states, states.Length * 2);
                    states[stateCount++] = new State { X = nx, Y = ny, Dir = current.Dir, Cost = newCost };
                }
            }

            // Turn left (counter-clockwise)
            int newDirLeft = (current.Dir - 1 + 4) % 4;
            int newCostLeft = current.Cost + 1000;
            if (newCostLeft < dist[current.X, current.Y, newDirLeft])
            {
                dist[current.X, current.Y, newDirLeft] = newCostLeft;
                if (stateCount == states.Length)
                    Array.Resize(ref states, states.Length * 2);
                states[stateCount++] = new State { X = current.X, Y = current.Y, Dir = newDirLeft, Cost = newCostLeft };
            }

            // Turn right (clockwise)
            int newDirRight = (current.Dir + 1) % 4;
            int newCostRight = current.Cost + 1000;
            if (newCostRight < dist[current.X, current.Y, newDirRight])
            {
                dist[current.X, current.Y, newDirRight] = newCostRight;
                if (stateCount == states.Length)
                    Array.Resize(ref states, states.Length * 2);
                states[stateCount++] = new State { X = current.X, Y = current.Y, Dir = newDirRight, Cost = newCostRight };
            }
        }

        Console.WriteLine("No path found to the end position.");
    }
}