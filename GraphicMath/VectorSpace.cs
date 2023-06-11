using EngineExceptions;

namespace GraphicsMath;

public class VectorSpace
{
    private readonly List<Vector> _basis;
    private readonly int _dimOfVectors;

    public VectorSpace(int dimOfVectors, List<Vector> data)
    {
        if (data.Any(vector => vector.Count != dimOfVectors) || dimOfVectors != data.Count)
            throw new EngineException.ObjectDimensionException();

        var matrix = new Matrix(data.Count, data.Count);
        for (var i = 0; i < data.Count; i++)
            matrix[i] = data[i];
        if (matrix.Determinant() == 0f)
            throw new EngineException.BasisContainsLinearlyDependentVectors();
        
        _dimOfVectors = dimOfVectors;

        _basis = data;
    }

    public int Count => _basis.Count;

    public Vector this[int i]
    {
        get
        {
            if (i < 0 || i >= _basis.Count)
                throw new EngineException.OutOfObjectException();

            return _basis[i];
        }
        set
        {
            if (i < 0 || i >= _basis.Count)
                throw new EngineException.OutOfObjectException();
            
            _basis[i] = value;
        }
    }

    public float ScalarProduct(Vector v1, Vector v2)
    {
        if (v1.Count != v2.Count || v1.Count != _dimOfVectors || v2.Count != _dimOfVectors)
            throw new EngineException.ObjectDimensionException();
        
        var vectors = new Vector[_basis.Count];
        _basis.CopyTo(vectors);
        return (v1.AsMatrix() * Matrix.Gram(vectors) * v2.TransposeAsMatrix())[0, 0];
    }
    
    public Vector VectorProduct(Vector v1, Vector v2)
    {
        if (v1.Count != 3 || v2.Count != 3) 
            throw new EngineException.ObjectDimensionException();

        Vector result = new Vector(3);

        result += _basis[0] * (v1[1] * v2[2] - 
                               v1[2] * v2[1]);

        result += _basis[1] * (v1[2] * v2[0] -
                               v1[0] * v2[2]);
        
        result += _basis[2] * (v1[0] * v2[1] -
                               v1[1] * v2[0]);

        return result;
    }

    public Vector AsVector(Point pt)
    {
        if (pt.Count != _basis.Count || pt.Count != _dimOfVectors)
            throw new EngineException.ObjectDimensionException();
        
        var res = new Vector(_basis.Count);
        
        for (var i = 0; i < _basis.Count; i++)
            res += _basis[i] * pt[i];
        
        return res;
    }

    public float Length(Vector vec) => (float)Math.Sqrt(ScalarProduct(vec, vec));
    
    public Vector Normalize(Vector vector) => vector / Length(vector);

    public override bool Equals(Object? obj)
    {
        if (obj == null)
            throw new NullReferenceException();
        
        if (!this.GetType().Equals(obj.GetType()))
            throw new EngineException.DifferentTypesException();
        
        VectorSpace vs = (VectorSpace)obj;
        
        if (Count != vs.Count)
            return false;

        for (var i = 0; i < Count; i++)
            if (!this[i].Equals(vs[i]))
                return false;

        return true;
    }
}