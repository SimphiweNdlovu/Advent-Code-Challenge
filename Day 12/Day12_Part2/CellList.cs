class CellList
{
    private Cell[] _items;
    private int _size;

    public CellList()
    {
        _items = new Cell[16];
        _size = 0;
    }

    public void Add(int row, int col)
    {
        if (_size == _items.Length)
            Resize();
        _items[_size++] = new Cell(row, col);
    }

    public Cell this[int index] => _items[index];

    public int Count => _size;

    private void Resize()
    {
        int newCapacity = _items.Length * 2;
        var newArr = new Cell[newCapacity];
        for (int i = 0; i < _items.Length; i++)
            newArr[i] = _items[i];
        _items = newArr;
    }
}


