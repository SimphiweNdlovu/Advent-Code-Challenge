
class StackList {
    private int[] _items;
    private int _size;

    public StackList() {
        _items = new int[16];
        _size = 0;
    }

    public void Add(int value) {
        if (_size == _items.Length)
            Resize();
        _items[_size++] = value;
    }

    public int Pop() {
        return _items[--_size];
    }

    public int Count {
        get { return _size; }
    }

    private void Resize() {
        int newCapacity = _items.Length * 2;
        int[] newArr = new int[newCapacity];
        for (int i = 0; i < _items.Length; i++) {
            newArr[i] = _items[i];
        }
        _items = newArr;
    }
}
//1518548
//