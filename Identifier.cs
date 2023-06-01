using System.Text;

namespace Engine;

public class Identifier
{
    public static readonly HashSet<int> Identifiers = new();
    private readonly int _id;

    public Identifier()
    {
        _id = __generate__();
        Identifiers.Add(_id);
    }

    public int Id => _id;

    private static int __generate__()
    {
        var rnd = new Random();
        var value = rnd.Next(); 
        while (Identifiers.Any(item => value == item))
        {
            value = rnd.Next(); 
        }

        return value;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var i in Identifiers)
            sb.Append(i + " ");

        return sb.ToString();
    }
}