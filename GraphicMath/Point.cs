using System.Text;
using EngineExceptions;

namespace GraphicsMath;

public class Point
{
    private int _dim;
    private float[] _data;
    public Point(int dim)
    {
        _dim = dim;
        _data = new float[dim];
        for (var i = 0; i < dim; i++)
            _data[i] = 0;
    }

    public Point(float[] values) : this(values.Length)
    {
        for (var i = 0; i < values.Length; i++)
            _data[i] = values[i];
    }

    public int Count => _dim;

    public float this[int i]
    {
        get
        {
            if (i < 0 || i >= _dim)
                throw new EngineException.OutOfObjectException();

            return _data[i];
        }
        set
        {
            if (i < 0 || i >= _dim)
                throw new EngineException.OutOfObjectException();

            _data[i] = value;
        }
    }

    public static Point Addition(Vector v, Point pt)
    {
        if (pt.Count != v.Count)
            throw new EngineException.ObjectDimensionException();

        var res = new Point(pt.Count);
        for (var i = 0; i < pt.Count; i++)
            res[i] = pt[i] + v[i];

        return res;
    }

    public static Point operator +(Point pt, Vector v) => Addition(v, pt);

    public static Point operator -(Point pt, Vector v) => Addition((-1) * v, pt);

    public override string ToString()
    {   
        var sb = new StringBuilder();
        for (var i = 0; i < _dim; i++)
            sb.Append(this[i] + " ");

        return sb.ToString();
    }
    
    public override bool Equals(Object? obj)
    {
        if (obj == null)
            throw new NullReferenceException();
        
        if (!this.GetType().Equals(obj.GetType()))
            throw new EngineException.DifferentTypesException();
        
        Point pt = (Point)obj;
        
        if (_dim != pt.Count)
            return false;

        for (var i = 0; i < _dim; i++)
            if (this[i] != pt[i])
                return false;

        return true;
    }
}