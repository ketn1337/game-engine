using System.Text;
using EngineExceptions;

namespace GraphicsMath;

public class Vector
{
    private readonly int _dim;
    private readonly float[] _data;
    public Vector(int dim)
    {
        _dim = dim;
        _data = new float[dim];
        for (var i = 0; i < dim; i++)
            _data[i] = 0;
    }
    
    public Vector(float[] values) : this(values.Length)
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

    public float ScalarProduct(Vector v)
    {
        if (Count != v.Count)
            throw new EngineException.ObjectDimensionException();
        
        return Matrix.Identity(Count).BilinearForm(this, v);
    }

    public Vector VectorProduct(Vector v)
    {
        if (Count != 3 || v.Count != 3)
            throw new EngineException.ObjectDimensionException();

        return new Vector(new float[3] 
            { 
                this[1] * v[2] - this[2] * v[1],
                -this[0] * v[2] + this[2] * v[0],
                this[0] * v[1] - this[1] * v[0]
            }
        );
    }

    public float Length() => 
        (float) System.Math.Sqrt(ScalarProduct(this));
    

    public void Normalize()
    {
        var length = Length();
        for (var i = 0; i < _dim; i++)
            this[i] /= length;
    }

    public Matrix AsMatrix()
    {
        var res = new Matrix(1, _dim);
        for (var i = 0; i < _dim; i++)
            res[0, i] = this[i];

        return res;
    }

    public Matrix TransposeAsMatrix() => this.AsMatrix().Transpose();
    
    public static Vector Addition(Vector v1, Vector v2)
    {
        if (v1.Count != v2.Count)
            throw new EngineException.ObjectDimensionException();

        var res = new Vector(v1.Count);
        for (var i = 0; i < v1.Count; i++)
            res[i] = v1[i] + v2[i];

        return res;
    }

    public static Vector MultiplicationByScalar(Vector v, float scalar)
    {
        var res = new Vector(v.Count);
        for (var i = 0; i < v.Count; i++)
            res[i] = v[i] * scalar;

        return res;
    }

    public static float operator %(Vector v1, Vector v2) => v1.ScalarProduct(v2);

    public static Vector operator ^(Vector v1, Vector v2) => v1.VectorProduct(v2);

    public static Vector operator +(Vector v1, Vector v2) => Addition(v1, v2);

    public static Vector operator -(Vector v1, Vector v2) => Addition(v1, MultiplicationByScalar(v2, -1));

    public static Vector operator *(Vector v, float num) => MultiplicationByScalar(v, num);

    public static Vector operator *(float num, Vector v) => MultiplicationByScalar(v, num);

    public static Vector operator /(Vector v, float num)
    {
        if (num == 0f)
            throw new DivideByZeroException();
        
        return MultiplicationByScalar(v, 1 / num);
    }
    
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
        
        Vector v = (Vector)obj;
        
        if (_dim != v.Count)
            return false;

        for (var i = 0; i < _dim; i++)
            if (this[i] != v[i])
                return false;

        return true;
    }
}