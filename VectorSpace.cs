using System.Numerics;

namespace engine;

public class VectorSpace
{
    private readonly List<Vector> _data = new List<Vector>();

    public VectorSpace(List<Vector> data)
    {
        foreach (var item in data)
        {
            _data.Add(item);
        }
    }

    public float ScalarProduct(Vector v1, Vector v2)
    {
        var vectors = new Vector[_data.Count];
        _data.CopyTo(vectors);
        return (v1.Transpose() * Matrix.Gram(vectors) * v2)[0, 0];
    }

    public Matrix AsVector(Point pt)
    {
        var res = new Matrix(_data.Count, 1);
        for (var i = 0; i < _data.Count; i++)
        {
            res += pt[i, 0] * _data[i];
        }

        return res;
    }
}