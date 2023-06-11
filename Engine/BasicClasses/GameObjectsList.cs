using EngineExceptions;

namespace Engine;

public class GameObjectsList
{
    private readonly List<GameObject> _obj;
    private List<string> _ids = new ();

    public GameObjectsList(List<GameObject> obj)
    {
        _obj = obj;
        foreach (var e in _obj)
            _ids.Add(e.Id);
    }

    public int Count => _obj.Count;

    public void Append(GameObject obj)
    {
        _obj.Add(obj);
        _ids.Add(obj.Id);
    }

    public void Remove(GameObject obj)
    {
        if (_obj.Any(e => !e.Equals(obj)) || _obj.Count == 0)
            throw new EngineException.ObjectNotIncludeItem();
        
        _obj.Remove(obj);
        _ids.Remove(obj.Id);
    }

    public GameObject Get(Identifier identifiers)
    {
        if (_ids.Count == 0)
            throw new EngineException.ObjectNotIncludeItem();
        
        if (identifiers.Id != _ids[^1]) //todo condition
            throw new EngineException.ObjectNotIncludeItem();

        foreach (var e in _obj)
        {
            if (e.Id == identifiers.Id)
                return e;
        }

        return null!;
    }

    public GameObject this[Identifier identifiers] => Get(identifiers);

    public virtual GameObject this[int i]
    {
        get
        {
            if (i < 0 || i >= _obj.Count)
                throw new EngineException.OutOfObjectException();
            
            return _obj[i];
        }
    }
    
    public void Exec(Action<GameObject> func)
    {
        foreach (var obj in _obj)
            func(obj);
    }
}