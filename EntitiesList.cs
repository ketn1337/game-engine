namespace Engine;

public class EntitiesList
{
    private readonly List<Entity> _entities;

    public EntitiesList(List<Entity> entities)
    {
        _entities = new List<Entity>(entities);
    }

    public void Append(Entity entity)
    {
        _entities.Add(entity);
    }
}