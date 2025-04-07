using System;
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

        int[,] map = new int[rows, cols];
        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                map[r, c] = lines[r][c] - '0';
            }
        }

        int total = 0;

        for (int startR = 0; startR < rows; startR++) {
            for (int startC = 0; startC < cols; startC++) {
                if (map[startR, startC] != 0) continue;

                bool[,] visited = new bool[rows, cols];
                visited[startR, startC] = true;
                int score = Explore(map, visited, startR, startC, rows, cols);
                total += score;
            }
        }

        Console.WriteLine(total);
    }

    static int Explore(int[,] map, bool[,] visited, int r, int c, int rows, int cols) {
        int height = map[r, c];
        int count = 0;
        if (height == 9) count++;

        int dr1 = -1, dc1 = 0;
        int dr2 = 1, dc2 = 0;
        int dr3 = 0, dc3 = -1;
        int dr4 = 0, dc4 = 1;

        int[] dr = { dr1, dr2, dr3, dr4 };
        int[] dc = { dc1, dc2, dc3, dc4 };

        for (int i = 0; i < 4; i++) {
            int nr = r + dr[i];
            int nc = c + dc[i];

            if (nr < 0 || nr >= rows || nc < 0 || nc >= cols) continue;
            if (visited[nr, nc]) continue;
            if (map[nr, nc] == height + 1) {
                visited[nr, nc] = true;
                count += Explore(map, visited, nr, nc, rows, cols);
            }
        }

        return count;
    }
}