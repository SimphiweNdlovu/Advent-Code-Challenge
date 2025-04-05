
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

    public int this[int index]
    {
        get { return _items[index]; }
    }

    public int Count
    {
        get { return _size; }
    }

    public void Sort()
    {
        Array.Sort(_items, 0, _size);
    }

    private void Resize()
    {
        int newCapacity = _items.Length * 2;
        int[] newArr = new int[newCapacity];
        for (int i = 0; i < _items.Length; i++)
        {
            newArr[i] = _items[i];
        }
        _items = newArr;
    }
}