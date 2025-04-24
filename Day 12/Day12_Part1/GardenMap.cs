
class GardenMap
{
    private readonly char[,] _map;
    private readonly bool[,] _visited;
    private readonly int _rows;
    private readonly int _cols;

    public GardenMap(string[] lines)
    {
        _rows = lines.Length;
        _cols = lines[0].Length;
        _map = new char[_rows, _cols];
        _visited = new bool[_rows, _cols];

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                _map[r, c] = lines[r][c];
            }
        }
    }

    public int CalculateTotalFenceCost()
    {
        int total = 0;
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                if (_visited[r, c]) continue;

                Region region = ExploreRegion(r, c);
                total += region.Area * region.Perimeter;
            }
        }
        return total;
    }

    private Region ExploreRegion(int startR, int startC)
    {
        char plant = _map[startR, startC];
        int area = 0;
        int perimeter = 0;

        StackList stackR = new StackList();
        StackList stackC = new StackList();

        stackR.Add(startR);
        stackC.Add(startC);
        _visited[startR, startC] = true;

        while (stackR.Count > 0)
        {
            int r = stackR.Pop();
            int c = stackC.Pop();

            area++;
            perimeter += CountPerimeterAndPushNeighbors(r, c, plant, stackR, stackC);
        }

        return new Region(area, perimeter);
    }

    private int CountPerimeterAndPushNeighbors(int r, int c, char plant, StackList stackR, StackList stackC)
    {
        int sides = 0;

        if (IsPerimeterOrPush(r - 1, c, plant, stackR, stackC)) sides++; // UP
        if (IsPerimeterOrPush(r + 1, c, plant, stackR, stackC)) sides++; // DOWN
        if (IsPerimeterOrPush(r, c - 1, plant, stackR, stackC)) sides++; // LEFT
        if (IsPerimeterOrPush(r, c + 1, plant, stackR, stackC)) sides++; // RIGHT

        return sides;
    }

    private bool IsPerimeterOrPush(int r, int c, char plant, StackList stackR, StackList stackC)
    {
        if (!IsInBounds(r, c) || _map[r, c] != plant)
        {
            return true; // Counts as perimeter
        }

        if (!_visited[r, c])
        {
            _visited[r, c] = true;
            stackR.Add(r);
            stackC.Add(c);
        }

        return false; // Not a perimeter side
    }

    private bool IsInBounds(int r, int c)
    {
        return r >= 0 && r < _rows && c >= 0 && c < _cols;
    }
}
