namespace engine;

public class Vector : Matrix
{
    public int Count
    {
        get
        {
            var (n, _) = Size;
            return n;
        }
    }

    public Vector(int n) : base(n, 1)
    {
    }
    
    public Vector(float[] values) : base(values.Length, 1, new List<List<float>> { values.ToList() })
    {
    }
    
    //todo vectors peregruzki

    public float ScalarProduct(Vector v)
    {
        if (Count != v.Count)
            throw new Exception("Vectors are different size");
        //todo bilinearform
        var res = 0f;
        for (var i = 0; i < Count; i++)
        {
            res += this[i, 0] * v[i, 0];
        }

        return res;
    }

    public Vector VectorProduct(Vector v)
    {
        if (Count != 3 && v.Count != 3)
            throw new Exception("Size of Vectors is not 3");

        return new Vector(new[]
        {
            this[1, 0] * v[2, 0] - this[2, 0] * v[1, 0],
            -this[0, 0] * v[2, 0] + this[2, 0] * v[0, 0],
            this[0, 0] * v[1, 0] - this[1, 0] * v[0, 0]
        });
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