class StackList
{
    private (int, int)[] _items;
    private int _size;

    public StackList()
    {
        _items = new (int, int)[16];
        _size = 0;
    }

    public void Add(int row, int col)
    {
        if (_size == _items.Length)
            Resize();
        _items[_size++] = (row, col);
    }

    public (int, int) Pop()
    {
        return _items[--_size];
    }

    public bool IsEmpty()
    {
        return _size == 0;
    }

    public int Count => _size;

    private void Resize()
    {
        int newCapacity = _items.Length * 2;
        var newArr = new (int, int)[newCapacity];
        for (int i = 0; i < _items.Length; i++)
            newArr[i] = _items[i];
        _items = newArr;
    }
}
