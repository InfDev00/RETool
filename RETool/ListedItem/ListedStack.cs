namespace RETool.ListedItem;

public class ListedStack<T>
{
    private readonly List<T> _list;
    
    public int Count => _list.Count;

    public T this[int index] => _list[index];
    
    #region Constructor

    public ListedStack() => _list = new List<T>();
    public ListedStack(IEnumerable<T> collection) => _list = new List<T>(collection);

    #endregion
    
    public void Clear() => _list.Clear();
    public bool Contains(T value) => _list.Contains(value);

    public T Pop()
    {
        var ret = _list[^1];
        _list.RemoveAt(_list.Count - 1);
        return ret;
    }
    public bool TryPop(out T? result)
    {
        try
        {
            result = Pop();
            return true;
        }
        catch(Exception)
        {
            result = default;
            return false;
        }
    }

    public void Push(T value) => _list.Add(value);
    public T Peek() => _list[^1];

    public bool TryPeek(out T? result)
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
    
    public T[] ToArray()
    {
        var ret = new T[_list.Count];

        for (var i = 0; i < _list.Count; i++) ret[i] = _list[i];

        return ret;
    }
}