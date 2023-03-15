namespace engine;

public class Vector
{
    private double _c1, _c2, _c3;
    public Vector(double c1, double c2, double c3)
    {
        _c1 = c1;
        _c2 = c2;
        _c3 = c3;
    }
    public Vector(Point pt)
    {
        (_c1, _c2, _c3) = pt.Coord;
    }

    public double Length { get => Math.Sqrt(
        Math.Pow(_c1, 2) +
        Math.Pow(_c2, 2) +
        Math.Pow(_c3, 2)); }
    
    public void Norm()
    {
        var len = Length;
        _c1 /= len;
        _c2 /= len;
        _c3 /= len;
    }
    
    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector(v1._c1 + v2._c1, v1._c2 + v2._c2, v1._c3 + v2._c3);
    }

    public static Vector operator *(Vector v1, Vector v2)
    {
        return new Vector(v1._c2 * v2._c3 - v1._c3 * v2._c2, -v1._c1 * v2._c3 + v1._c3 * v2._c1, v1._c1 * v2._c2 - v1._c2 * v2._c1);
    }
    
    public static Vector operator *(Vector v, double num)
    {
        return new Vector(v._c1 * num,  v._c2 * num, v._c3 * num);
    }
    
    public static Vector operator *(double num, Vector v)
    {
        return new Vector(v._c1 * num,  v._c2 * num, v._c3 * num);
    }
    
    public static Vector operator -(Vector v1, Vector v2)
    {
        return new Vector(v1._c1 - v2._c1,  v1._c2 - v2._c2, v1._c3 - v2._c3);
    }
    
    public static Vector operator /(Vector v, double num)
    {
        return new Vector(v._c1 / num,  v._c2 / num, v._c3 / num);
    }

    public static double operator ^(Vector v1, Vector v2)
    {
        return v1._c1 * v2._c1 + v1._c2 * v2._c2 + v1._c3 * v2._c3;
    }

    public void Print()
    {
        Console.WriteLine(_c1 + " " + _c2 + " " + _c3);
    }
}