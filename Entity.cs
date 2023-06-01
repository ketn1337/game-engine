using GraphicsMath;

namespace Engine;

public class Entity
{
    private CoordinateSystem _cs;
    private readonly Identifier _idSet;
    private readonly Dictionary<string, object> _props = new Dictionary<string, object>();

    public Entity(CoordinateSystem cs)
    {
        _cs = cs;
        _idSet = new Identifier();
    }

    public int Id => _idSet.Id;

    public object this[string prop]
    {
        get => GetProperty(prop);
        set => SetProperty(prop, value);
    }

    public void SetProperty(string prop, object value) => _props.Add(prop, value);

    public object GetProperty(string prop)
    {
        if (!_props.ContainsKey(prop))
            throw new EngineException.ObjectNotIncludeItem();
            
        return _props[prop];
    }

    public void RemoveProp(string prop)
    {
        if (!_props.ContainsKey(prop))
            throw new EngineException.ObjectNotIncludeItem();
        
        _props.Remove(prop);
    }
}