
class IntList
{
    private int[] _items;
    private int _size;

    public IntList()
    {
        _items = new int[8];
        _size = 0;
    }

    public void Add(int value)
    {
        if (_size == _items.Length)
            Resize();
        _items[_size++] = value;
    }

    public int this[int index] { get { return _items[index]; } }
    public int Count { get { return _size; } }

    private void Resize()
    {
        int[] newArr = new int[_items.Length * 2];
        for (int i = 0; i < _items.Length; i++)
            newArr[i] = _items[i];
        _items = newArr;
    }

    public int Sum()
    {
        int total = 0;
        for (int i = 0; i < _size; i++)
            total += _items[i];
        return total;
    }
}
