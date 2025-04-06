class Rule
{
    public int Before;
    public int After;
}

class RuleList
{
    private Rule[] _items;
    private int _size;

    public RuleList()
    {
        _items = new Rule[16];
        _size = 0;
    }

    public void Add(int before, int after)
    {
        if (_size == _items.Length)
            Resize();

        _items[_size++] = new Rule { Before = before, After = after };
    }

    public Rule this[int index] { get { return _items[index]; } }
    public int Count { get { return _size; } }

    private void Resize()
    {
        Rule[] newArr = new Rule[_items.Length * 2];
        for (int i = 0; i < _items.Length; i++)
            newArr[i] = _items[i];
        _items = newArr;
    }
}