using EngineExceptions;

namespace Engine;

public class EntitiesList
{
    private readonly List<Entity> _entities;
    private List<string> _ids = new ();

    public EntitiesList(List<Entity> entities)
    {
        _entities = entities;
        foreach (var e in entities)
            _ids.Add(e.Id);
    }

    public int Count => _entities.Count;

    public void Append(Entity entity)
    {
        _entities.Add(entity);
        _ids.Add(entity.Id);
    }

    public void Remove(Entity entity)
    {
        if (_entities.Any(e => !e.Equals(entity)) || _entities.Count == 0)
            throw new EngineException.ObjectNotIncludeItem();
        
        _entities.Remove(entity);
        _ids.Remove(entity.Id);
    }

    public Entity Get(Identifier identifiers)
    {
        if (_ids.Count == 0)
            throw new EngineException.ObjectNotIncludeItem();
        
        if (identifiers.Id != _ids[^1]) //todo condition
            throw new EngineException.ObjectNotIncludeItem();

        foreach (var e in _entities)
        {
            if (e.Id == identifiers.Id)
                return e;
        }

        return null!;
    }

    public Entity this[Identifier identifiers] => Get(identifiers);

    public virtual Entity this[int i]
    {
        get
        {
            if (i < 0 || i >= _entities.Count)
                throw new EngineException.OutOfObjectException();
            
            return _entities[i];
        }
    }
    
    public void Exec(Action<Entity> func)
    {
        foreach (var entity in _entities)
            func(entity);
    }
}