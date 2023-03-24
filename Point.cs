namespace engine;

public class Point : Vector
{
    private readonly int _n;

    public Point(int n) : base(n)
    {
        _n = n;
    }

    public Point(int n, params float[] args) : base(n, args)
    {
        _n = n;
    }

    public static Point operator +(Point pt, Vector v)
    {
        if (pt._n != v.Count)
            throw new Exception("Different size of Vector and Point");

        var res = new Point(pt._n);
        for (var i = 0; i < pt._n; i++)
        {
            res[i, 0] = pt[i, 0] + v[i, 0];
        }

        return res;
    }
    
    public static Point operator -(Point pt, Vector v)
    {
        if (pt._n != v.Count)
            throw new Exception("Different size of Vector and Point");

        var res = new Point(pt._n);
        for (var i = 0; i < pt._n; i++)
        {
            res[i, 0] = pt[i, 0] - v[i, 0];
        }

        return res;
    }
}
