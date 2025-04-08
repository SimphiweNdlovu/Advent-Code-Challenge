using System;
using System.IO;
using System.Collections.Generic;

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

        int[,] map = new int[rows, cols];
        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                map[r, c] = lines[r][c] - '0';
            }
        }

        int totalRating = 0;

        // Loop through the entire map to find all trailheads (height 0)
        for (int startR = 0; startR < rows; startR++) {
            for (int startC = 0; startC < cols; startC++) {
                if (map[startR, startC] != 0) continue; // Only consider trailheads (height 0)

                bool[,] visited = new bool[rows, cols];
                visited[startR, startC] = true;

                // Set to track distinct trails from the current trailhead
                HashSet<string> distinctTrails = new HashSet<string>();
                ExploreDistinctTrails(map, visited, startR, startC, rows, cols, distinctTrails, $"{startR},{startC}");

                // Add the number of distinct trails to the total rating
                totalRating += distinctTrails.Count;
            }
        }

        Console.WriteLine(totalRating);
    }

    static void ExploreDistinctTrails(int[,] map, bool[,] visited, int r, int c, int rows, int cols, HashSet<string> distinctTrails, string path) {
        // Directions for moving (up, down, left, right)
        int[] dr = {-1, 1, 0, 0};
        int[] dc = { 0, 0, -1, 1 };

        // If we are at a height of 9, it's a valid end of a trail
        if (map[r, c] == 9) {
            distinctTrails.Add(path);  // Add the full trail path to the set
            return;
        }

        // Explore all four directions
        for (int i = 0; i < 4; i++) {
            int nr = r + dr[i];
            int nc = c + dc[i];

            // Check if the new position is within bounds and hasn't been visited
            if (nr < 0 || nr >= rows || nc < 0 || nc >= cols || visited[nr, nc]) continue;

            // Only move to the next step if it's an increment of 1 in height
            if (map[nr, nc] == map[r, c] + 1) {
                visited[nr, nc] = true; // Mark as visited before exploring

                // Perform the recursive exploration, passing the updated path
                ExploreDistinctTrails(map, visited, nr, nc, rows, cols, distinctTrails, path + $" -> {nr},{nc}");

                visited[nr, nc] = false; // Unmark as visited after exploring
            }
        }
    }
}
