namespace engine;

public class Point
{
    private readonly double _c1, _c2, _c3;
    public Point(double c1, double c2, double c3)
    {
        _c1 = c1;
        _c2 = c2;
        _c3 = c3;
    }
    public void Deconstruct(out double c1, out double c2, out double c3)
    {
        c1 = _c1;
        c2 = _c2;
        c3 = _c3;
    }
    //public (double, double, double) Coord { get => (_c1, _c2, _c3); }
    /*public static void test()
    {
        Func<Point, Point, double>[] function = {Distance};
        Console.WriteLine(function[0](new Point(1, 1, 1), new Point(2, 2, 2)));
    }*/
    public double Distance(Point pt)
    {
        return Math.Sqrt(
            Math.Pow(Math.Abs(_c1 - pt._c1), 2) + 
            Math.Pow(Math.Abs(_c2 - pt._c2), 2) + 
            Math.Pow(Math.Abs(_c3 - pt._c3), 2));
    }

    public static Point operator +(Point pt1, Point pt2)
    {
        return new Point(pt1._c1 + pt2._c1,  pt1._c2 + pt2._c2, pt1._c3 + pt2._c3);
    }
    
    public static Point operator *(Point pt, double num)
    {
        return new Point(pt._c1 * num,  pt._c2 * num, pt._c3 * num);
    }
    
    public static Point operator *(double num, Point pt)
    {
        return new Point(pt._c1 * num,  pt._c2 * num, pt._c3 * num);
    }
    
    public static Point operator -(Point pt1, Point pt2)
    {
        return new Point(pt1._c1 - pt2._c1,  pt1._c2 - pt2._c2, pt1._c3 - pt2._c3);
    }
    
    public static Point operator /(Point pt, double num)
    {
        return new Point(pt._c1 / num,  pt._c2 / num, pt._c3 / num);
    }
    
    public void Print()
    {
        Console.WriteLine(_c1 + " " + _c2 + " " + _c3);
    }
}
