class GardenCalculator
{
    public int CalculateTotalFenceCost(string[] lines)
    {
        int rows = lines.Length;
        int cols = lines[0].Length;

        char[,] map = new char[rows, cols];
        bool[,] visited = new bool[rows, cols];
        // Initialize the map and visited arrays
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                map[r, c] = lines[r][c];

        int total = 0;
        // Iterate through each cell in the map
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (visited[r, c]) continue;

                char plant = map[r, c];
                CellList cells = new CellList();
                bool[,] currentRegion = new bool[rows, cols];

                StackList stack = new StackList();
                stack.Add(r, c);
                visited[r, c] = true;
                currentRegion[r, c] = true;
                cells.Add(r, c);
                // Process the stack until it's empty
                while (stack.Count > 0)
                {   
                    
                    var (cr, cc) = stack.Pop();
                    ProcessNeighbors(cr, cc, plant, map, visited, currentRegion, cells, stack, rows, cols);

                }

                bool[,] topExposed = new bool[rows, cols];
                bool[,] bottomExposed = new bool[rows, cols];
                bool[,] leftExposed = new bool[rows, cols];
                bool[,] rightExposed = new bool[rows, cols];
                // Check for exposed sides
              CheckExposedSides(cells, plant, map, topExposed, bottomExposed, leftExposed, rightExposed, rows, cols);
                // Count segments for each side
                int topSegments = CountSegments(currentRegion, topExposed, rows, cols, horizontal: true);
                int bottomSegments = CountSegments(currentRegion, bottomExposed, rows, cols, horizontal: true);
                int leftSegments = CountSegments(currentRegion, leftExposed, rows, cols, horizontal: false);
                int rightSegments = CountSegments(currentRegion, rightExposed, rows, cols, horizontal: false);

                int totalSides = topSegments + bottomSegments + leftSegments + rightSegments;
                int area = cells.Count;
                total += area * totalSides;
            }
        }

        return total;
    }

    private int CountSegments(bool[,] region, bool[,] exposed, int rows, int cols, bool horizontal)
    {
        int segments = 0;
        // Count segments in the specified direction
        if (horizontal)
        {
            for (int row = 0; row < rows; row++)
            {
                bool prev = false;
                for (int col = 0; col < cols; col++)
                {
                    if (region[row, col] && exposed[row, col])
                    {
                        if (!prev)
                        {
                            segments++;
                            prev = true;
                        }
                    }
                    else
                    {
                        prev = false;
                    }
                }
            }
        }
        else 
        {
            for (int col = 0; col < cols; col++)
            {
                bool prev = false;
                for (int row = 0; row < rows; row++)
                {
                    if (region[row, col] && exposed[row, col])
                    {
                        if (!prev)
                        {
                            segments++;
                            prev = true;
                        }
                    }
                    else
                    {
                        prev = false;
                    }
                }
            }
        }

        return segments;
    }

    private void ProcessNeighbors(int cr, int cc, char plant, char[,] map, bool[,] visited, bool[,] currentRegion, CellList cells, StackList stack, int rows, int cols)
{
    // Up
    if (cr - 1 >= 0 && map[cr - 1, cc] == plant && !visited[cr - 1, cc])
    {
        visited[cr - 1, cc] = true;
        currentRegion[cr - 1, cc] = true;
        stack.Add(cr - 1, cc);
        cells.Add(cr - 1, cc);
    }
    // Down
    if (cr + 1 < rows && map[cr + 1, cc] == plant && !visited[cr + 1, cc])
    {
        visited[cr + 1, cc] = true;
        currentRegion[cr + 1, cc] = true;
        stack.Add(cr + 1, cc);
        cells.Add(cr + 1, cc);
    }
    // Left
    if (cc - 1 >= 0 && map[cr, cc - 1] == plant && !visited[cr, cc - 1])
    {
        visited[cr, cc - 1] = true;
        currentRegion[cr, cc - 1] = true;
        stack.Add(cr, cc - 1);
        cells.Add(cr, cc - 1);
    }
    // Right
    if (cc + 1 < cols && map[cr, cc + 1] == plant && !visited[cr, cc + 1])
    {
        visited[cr, cc + 1] = true;
        currentRegion[cr, cc + 1] = true;
        stack.Add(cr, cc + 1);
        cells.Add(cr, cc + 1);
    }
}

private void CheckExposedSides(CellList cells, char plant, char[,] map, bool[,] topExposed, bool[,] bottomExposed, bool[,] leftExposed, bool[,] rightExposed, int rows, int cols)
{
    for (int i = 0; i < cells.Count; i++)
    {
        int cr = cells[i].Row;
        int cc = cells[i].Col;

        // Check for exposed sides
        // Check if the cell is at the edge of the map or if the adjacent cell is not the same plant
        
        // Check top side
        if (cr - 1 < 0 || map[cr - 1, cc] != plant)
            topExposed[cr, cc] = true;

        // Check bottom side
        if (cr + 1 >= rows || map[cr + 1, cc] != plant)
            bottomExposed[cr, cc] = true;
        // Check left side
        if (cc - 1 < 0 || map[cr, cc - 1] != plant)
            leftExposed[cr, cc] = true;
        // Check right side
        if (cc + 1 >= cols || map[cr, cc + 1] != plant)
            rightExposed[cr, cc] = true;
    }
}


}
