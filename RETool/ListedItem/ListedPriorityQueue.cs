namespace RETool.ListedItem;

public class ListedPriorityQueue<TElement, TPriority>
{
    private readonly List<Tuple<TElement, TPriority>> _list;
    private readonly Func<TPriority, TPriority, int> _comparer;

    public int Count => _list.Count;
    
    #region Constructor

    public ListedPriorityQueue()
    {
        _comparer = Comparer<TPriority>.Default.Compare;
        _list = new List<Tuple<TElement, TPriority>>();
    }

    public ListedPriorityQueue(Func<TPriority, TPriority, int> customComparer)
    {
        _comparer = customComparer;
        _list = new List<Tuple<TElement, TPriority>>();
    }
    #endregion
    
    public void Clear() => _list.Clear();
    public bool Contains(TElement value) => _list.Any(tuple => Equals(tuple.Item1, value));

    public void Enqueue(TElement value, TPriority priority)
    {
        var index = _list.Count;
        _list.Add(new Tuple<TElement, TPriority>(value, priority));
        
        while (index > 0)
        {
            var parentIndex = (index - 1) / 2;
            
            if(_comparer(_list[index].Item2, _list[parentIndex].Item2) <= 0) break;//parent is bigger or same;

            (_list[index], _list[parentIndex]) = (_list[parentIndex], _list[index]);
            index = parentIndex;
        }
    }

    public TElement Dequeue()
    {
        var ret = _list[0].Item1;
        _list[0] = _list[^1];
        _list.RemoveAt(_list.Count - 1);

        var index = 0;

        while (index < _list.Count)
        {
            var leftIndex = index * 2 + 1;
            var rightIndex = index * 2 + 2;
            
            if(leftIndex >= _list.Count) break;
            if (rightIndex < _list.Count && _comparer(_list[leftIndex].Item2, _list[rightIndex].Item2) < 0)
                leftIndex = rightIndex;
            
            if(_comparer(_list[leftIndex].Item2, _list[index].Item2) <= 0) break;
            
            (_list[index], _list[leftIndex]) = (_list[leftIndex], _list[index]);
            index = leftIndex;
        }

        return ret;
    }
    
    public bool TryDequeue(out TElement? result)
    {
        try
        {
            result = Dequeue();
            return true;
        }
        catch(Exception)
        {
            result = default;
            return false;
        }
    }


    public TElement Peek()
    {
        return _list[0].Item1;
    }
    public bool TryPeek(out TElement? result)
    {
        try
        {
            result = Peek();
            return true;
        }
        catch(Exception)
        {
            result = default;
            return false;
        }
    }
}