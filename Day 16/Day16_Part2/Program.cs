using System;
using System.IO;
using System.Collections.Generic;

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

        // Locate start and end positions
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
        var directions = new (int dx, int dy)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };

        // Initialize distance array with maximum values
        int[,,] dist = new int[rows, cols, 4];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                for (int d = 0; d < 4; d++)
                    dist[i, j, d] = int.MaxValue;

        // Start facing east (direction 1)
        dist[startX, startY, 1] = 0;

        List<State> states = new List<State>();
        states.Add(new State { X = startX, Y = startY, Dir = 1, Cost = 0 });

        int minimalCost = int.MaxValue;

        // Forward Dijkstra's algorithm to compute minimal distances
        while (states.Count > 0)
        {
            // Find state with minimal cost
            int minIndex = 0;
            for (int i = 1; i < states.Count; i++)
                if (states[i].Cost < states[minIndex].Cost)
                    minIndex = i;

            State current = states[minIndex];
            states.RemoveAt(minIndex);

            if (current.Cost > dist[current.X, current.Y, current.Dir])
                continue;

            // Update minimal cost if current is the end
            if (current.X == endX && current.Y == endY)
                if (current.Cost < minimalCost)
                    minimalCost = current.Cost;

            // Move forward
            var (dx, dy) = directions[current.Dir];
            int nx = current.X + dx;
            int ny = current.Y + dy;
            if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && grid[nx][ny] != '#')
            {
                int newCost = current.Cost + 1;
                if (newCost < dist[nx, ny, current.Dir])
                {
                    dist[nx, ny, current.Dir] = newCost;
                    states.Add(new State { X = nx, Y = ny, Dir = current.Dir, Cost = newCost });
                }
            }

            // Turn left (counter-clockwise)
            int newDirLeft = (current.Dir - 1 + 4) % 4;
            int newCostLeft = current.Cost + 1000;
            if (newCostLeft < dist[current.X, current.Y, newDirLeft])
            {
                dist[current.X, current.Y, newDirLeft] = newCostLeft;
                states.Add(new State { X = current.X, Y = current.Y, Dir = newDirLeft, Cost = newCostLeft });
            }

            // Turn right (clockwise)
            int newDirRight = (current.Dir + 1) % 4;
            int newCostRight = current.Cost + 1000;
            if (newCostRight < dist[current.X, current.Y, newDirRight])
            {
                dist[current.X, current.Y, newDirRight] = newCostRight;
                states.Add(new State { X = current.X, Y = current.Y, Dir = newDirRight, Cost = newCostRight });
            }
        }

        if (minimalCost == int.MaxValue)
        {
            Console.WriteLine("No path found to the end.");
            return;
        }

        // Collect all end nodes with minimal cost
        var endNodes = new List<(int x, int y, int dir)>();
        for (int dir = 0; dir < 4; dir++)
            if (dist[endX, endY, dir] == minimalCost)
                endNodes.Add((endX, endY, dir));

        if (endNodes.Count == 0)
        {
            Console.WriteLine("No valid end directions found.");
            return;
        }

        // Reverse BFS to find all tiles in best paths
        var bestTiles = new HashSet<(int x, int y)>();
        var visited = new bool[rows, cols, 4];
        var queue = new Queue<(int x, int y, int dir)>();

        foreach (var node in endNodes)
        {
            queue.Enqueue(node);
            visited[node.x, node.y, node.dir] = true;
            bestTiles.Add((node.x, node.y));
        }

        while (queue.Count > 0)
        {
            var (x, y, dir) = queue.Dequeue();
            bestTiles.Add((x, y));

            // Check move predecessor
            var (dx, dy) = directions[dir];
            int px = x - dx;
            int py = y - dy;
            if (px >= 0 && px < rows && py >= 0 && py < cols && grid[px][py] != '#')
            {
                if (dist[px, py, dir] + 1 == dist[x, y, dir] && !visited[px, py, dir])
                {
                    visited[px, py, dir] = true;
                    queue.Enqueue((px, py, dir));
                    bestTiles.Add((px, py));
                }
            }

            // Check turn left predecessor (right turn in reverse)
            int pdirLeft = (dir + 1) % 4;
            if (dist[x, y, pdirLeft] + 1000 == dist[x, y, dir] && !visited[x, y, pdirLeft])
            {
                visited[x, y, pdirLeft] = true;
                queue.Enqueue((x, y, pdirLeft));
                bestTiles.Add((x, y));
            }

            // Check turn right predecessor (left turn in reverse)
            int pdirRight = (dir - 1 + 4) % 4;
            if (dist[x, y, pdirRight] + 1000 == dist[x, y, dir] && !visited[x, y, pdirRight])
            {
                visited[x, y, pdirRight] = true;
                queue.Enqueue((x, y, pdirRight));
                bestTiles.Add((x, y));
            }
        }

        Console.WriteLine(bestTiles.Count);
    }
}