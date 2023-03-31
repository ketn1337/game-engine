namespace engine;

public class Point : Vector
{
    public Point(int n) : base(n)
    {
    }

    public Point(float[] values) : base(values)
    {
    }

    public static Point operator +(Point pt, Vector v)
    {
        if (pt.Count != v.Count)
            throw new Exception("Different size of Vector and Point");

        var res = new Point(pt.Count);
        for (var i = 0; i < pt.Count; i++) res[i, 0] = pt[i, 0] + v[i, 0];

        return res;
    }
    
    public static Point operator -(Point pt, Vector v)
    {
        //todo return pt + (-1) * v;
        return new Point(pt.Count);
    }
    
    //todo empty public perezruzki
}