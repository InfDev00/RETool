namespace RETool.ListedItem;

public class ListedQueue<T>
{
    private readonly List<T> _list;
    
    public int Count => _list.Count;
    public T this[int index] => _list[index];

    #region Constructor

    public ListedQueue() => _list = new List<T>();
    public ListedQueue(IEnumerable<T> collection) => _list = new List<T>(collection);

    #endregion
    
    public void Clear() => _list.Clear();
    public bool Contains(T value) => _list.Contains(value);
    
    public T Dequeue()
    {
        var ret = _list[0];
        _list.RemoveAt(0);
        
        return ret;
    }

    public bool TryDequeue(out T? result)
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
    
    public void Enqueue(T value) => _list.Add(value);

    public T Peek() => _list[0];

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