class IntList
{
    private int[] _items;
    private int _size;

    public IntList()
    {
        _items = new int[4];
        _size = 0;
    }

    public void Add(int value)
    {
        if (_size == _items.Length)
            Resize();
        _items[_size++] = value;
    }

    private void Resize()
    {
        int newSize = _items.Length * 2;
        int[] newArray = new int[newSize];
        Array.Copy(_items, newArray, _items.Length);
        _items = newArray;
    }

    public int this[int index]
    {
        get { return _items[index]; }
    }

    public int Count => _size;

    public IntList RemoveAt(int index)
    {
        IntList result = new IntList();
        for (int i = 0; i < _size; i++)
        {
            if (i != index)
            {
                result.Add(_items[i]);
            }
        }
        return result;
    }
}