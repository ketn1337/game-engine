using System.Text;

namespace Engine;

public class Identifier
{
    private static readonly HashSet<string> Identifiers = new();
    private readonly string _id;

    public Identifier()
    {
        _id = __generate__();
        Identifiers.Add(_id);
    }

    public string Id => _id;
    public HashSet<string> Set => Identifiers;
    private static string __generate__() => Guid.NewGuid().ToString();

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var i in Identifiers)
            sb.Append(i + '\n');

        return sb.ToString();
    }
}