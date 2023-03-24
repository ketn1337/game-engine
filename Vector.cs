using System.Data;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace engine;

public class Vector : Matrix
{
    private readonly int _n;

    public int Count => _n;
    
    public Vector(int n) : base(n, 1)
    {
        _n = n;
    }
    
    public Vector(int n, params float[] args) : base(n, 1)
    {
        _n = n;
        for (var i = 0; i < args.Length; i++)
        {
            Data[i][0] = args[i];
        }
    }

    public float ScalarProduct(Vector v)
    {
        if (_n != v._n)
            throw new Exception("Vectors are different size");

        var res = 0f;
        for (var i = 0; i < _n; i++)
        {
            res += this[_n, 0] * v[_n, 0];
        }

        return res;
    }

    public Vector VectorProduct(Vector v)
    {
        if (_n != 3 && v._n != 3)
            throw new Exception("Size of Vectors is not 3");
        
        return new Vector(3, this[1, 0] * v[2, 0] - this[2, 0] * v[1, 0], -this[0, 0] * v[2, 0] + this[2, 0] * v[0, 0], this[0, 0] * v[1, 0] - this[1, 0] * v[0, 0]);
    }

    public float Length()
    {
        return (float) Math.Sqrt(ScalarProduct(this));
    }

    public static float operator %(Vector v1, Vector v2)
    {
        return v1.ScalarProduct(v2);
    }

    public static Vector operator ^(Vector v1, Vector v2)
    {
        return v1.VectorProduct(v2);
    }
}