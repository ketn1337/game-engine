namespace Engine;

public class KeyMapper
{
    readonly Dictionary<ConsoleKey, List<Tuple<Entity, Action<Entity>>>> _keyMapDict = new();

    public KeyMapper(MainLoop loop)
    {
        loop.KeyPress += KeyPress;
    }

    private void KeyPress(object? sender, ConsoleKeyInfo e)
    {
        if (_keyMapDict.ContainsKey(e.Key))
            foreach (var actionPair in _keyMapDict[e.Key])
            {
                var act = actionPair.Item2;
                if (act != null!) act(actionPair.Item1);
            }
    }

    public void Add(ConsoleKey key, Entity ent, Action<Entity> act)
    {
        Tuple<Entity, Action<Entity>> pair = new(ent, act);
        if (!_keyMapDict.ContainsKey(key)) _keyMapDict[key] = new List<Tuple<Entity, Action<Entity>>> { pair };
        else _keyMapDict[key].Add(pair);
    }

    public void Clear(ConsoleKey key)
    {
        _keyMapDict[key].Clear();
    }

    public void Remove(ConsoleKey key, Tuple<Entity, Action<Entity>> pair)
    {
        _keyMapDict[key].Remove(pair);
    }
}