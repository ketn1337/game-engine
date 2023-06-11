using System.Text;
using EngineExceptions;

namespace GraphicsMath;

public class Matrix
{
    private readonly int _rows, _columns;
    private readonly List<List<float>> _data = new();

    public Matrix(int n)
    {
        _rows = n;
        _columns = n;
        for (var j = 0; j < _rows; j++)
        {
            var line = new List<float>(_columns);
            for (var i = 0; i < _columns; i++) line.Add(0);
            _data.Add(line);
        }
    }
    
    public Matrix(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
        for (var j = 0; j < _rows; j++)
        {
            var line = new List<float>(_columns);
            for (var i = 0; i < _columns; i++) line.Add(0);
                _data.Add(line);
        }
    }
    
    public Matrix(int rows, int columns, List<List<float>> data) : this(rows, columns)
    {
        if (data.Count != _rows || data.Any(row => row.Count != _columns))
            throw new EngineException.ObjectDimensionException();

        for (var i = 0; i < _rows; i++)
            for (var j = 0; j < _columns; j++)
                _data[i][j] = data[i][j];
    }
    
    public (int, int) Size => (_rows, _columns);

    public float this[int i, int j]
    {
        get
        {
            if ((i < 0 || i >= _rows) || (j < 0 || j >= _columns))
                throw new EngineException.OutOfObjectException();
            
            return _data[i][j];
        }
        set
        {
            if ((i < 0 || i >= _rows) || (j < 0 || j >= _columns))
                throw new EngineException.OutOfObjectException();
            
            _data[i][j] = value;
        }
    }
    public Vector this[int i]
    {
        get
        {
            if (i < 0 || i >= _rows)
                throw new EngineException.OutOfObjectException();
            
            return new Vector(_data[i].ToArray());
        }
        set
        {
            if (i < 0 || i >= _rows || value.Count != _columns)
                throw new EngineException.OutOfObjectException();

            for (var j = 0; j < _columns; j++)
                this[i, j] = value[j];
        }
    }

    public Matrix GetMatrixWithoutRowAndColumn(int row, int column)
    {
        if (row >= _rows || row < 0 || column >= _columns || column < 0)
            throw new EngineException.OutOfObjectException();
        
        var res = new Matrix(_rows - 1, _columns - 1);
        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _columns; j++)
            {
                if (j < column && i < row) res[i, j] = this[i, j];
                if (j < column && i > row) res[i - 1, j] = this[i, j];
                if (j > column && i < row) res[i, j - 1] = this[i, j];
                if (j > column && i > row) res[i - 1, j - 1] = this[i, j];
            }
        }

        return res;
    }
    
    
    public double Determinant()
    {
        if (_rows != _columns)
            throw new EngineException.ObjectDimensionException();

        if (_rows == 1)
            return this[0, 0];
        

        double res = 0;
        for (var i = 0; i < _rows; i++)
        {
            var submatrix = this.GetMatrixWithoutRowAndColumn(0, i);
            res += System.Math.Pow(-1, i) * this[0, i] * submatrix.Determinant();
        }

        return res;
    }

    public Matrix Transpose()
    {
        var res = new Matrix(_columns, _rows);
        for (var i = 0; i < _rows; i++)
            for (var j = 0; j < _columns; j++)
                res[j, i] = this[i, j];

        return res; 
    }
    
    public Matrix Inverse()
    {
        if (Determinant() == 0.0)
            throw new EngineException.DeterminantIsZeroException();

        var cofactorMatrix = new Matrix(_rows, _columns);
        for (var i = 0; i < _rows; i++)
            for (var j = 0; j < _columns; j++)
            {
                var submatrix = this.GetMatrixWithoutRowAndColumn(i,j);
                cofactorMatrix[i, j] = (float) System.Math.Pow(-1, i + j) * (float) submatrix.Determinant();
            }

        return cofactorMatrix.Transpose() / (float) this.Determinant();
    }

    public static Matrix GetRotationMatrix(int dim, (int, int) axes, float angle)
    {
        if (axes.Item1 >= dim || axes.Item2 >= dim)
            throw new EngineException.OutOfObjectException(); // ?
        
        angle *= ((float) System.Math.PI / 180);
        
        var rotationMatrix = Identity(dim);
        rotationMatrix[axes.Item1, axes.Item1] = (float) System.Math.Cos(angle);
        rotationMatrix[axes.Item2, axes.Item2] = (float) System.Math.Cos(angle);
        rotationMatrix[axes.Item2, axes.Item1] = (float) -(System.Math.Pow(-1, axes.Item1 + axes.Item2) * System.Math.Sin(angle));
        rotationMatrix[axes.Item1, axes.Item2] = (float) (System.Math.Pow(-1, axes.Item1 + axes.Item2) * System.Math.Sin(angle));
        
        return rotationMatrix;
    }

    public static Matrix GetRotationMatrix_X(float angle) => 
        GetRotationMatrix(3, ( 1, 2 ), angle);
    
    public static Matrix GetRotationMatrix_Y(float angle) => 
        GetRotationMatrix(3, ( 0, 2 ), angle);
    
    public static Matrix GetRotationMatrix_Z(float angle) => 
        GetRotationMatrix(3, ( 0, 1 ), angle);

    public static Matrix Rotate3D(float x, float y, float z) =>
        GetRotationMatrix_X(x) * GetRotationMatrix_Y(y) * GetRotationMatrix_Z(z);

    public float BilinearForm(Vector v1, Vector v2)
    {
        if (v1.Count != _rows || v2.Count != _columns || _rows != _columns)
            throw new EngineException.ObjectDimensionException();
        
        var sum = 0f;
        for (var i = 0; i < _rows; i++)
            for (var j = 0; j < _columns; j++)
                sum += (this[i, j] * v1[i] * v2[j]);

        return sum;
    }

    public static Matrix Identity(int n)
    {
        var res = new Matrix(n, n);
        for (var i = 0; i < n; i++)
            res[i, i] = 1;

        return res;
    }

    public static Matrix Gram(params Vector[] args)
    {
        var dim = args[0].Count;
        foreach(var item in args)
            if (item.Count != dim)
                throw new EngineException.ObjectDimensionException();
        
        var res = new Matrix(args.Length, args.Length);

        for (var i = 0; i < args.Length; i++)
            for (var j = 0; j < args.Length; j++)
                res[i, j] = args[i].ScalarProduct(args[j]);
        
        return res;
    }
    
    public static Matrix Addition(Matrix m1, Matrix m2)
    {
        if (m1._rows != m2._rows || m1._columns != m2._columns)
            throw new EngineException.ObjectDimensionException();
        
        var res = new Matrix(m1._rows, m1._columns);
        for (var i = 0; i < m1._rows; i++)
        for (var j = 0; j < m1._columns; j++)
            res[i, j] = m1[i, j] + m2[i, j];
        
        return res;
    }

    public static Matrix MatrixMultiplication(Matrix m1, Matrix m2)
    {
        if (m1._columns != m2._rows)
            throw new EngineException.ObjectDimensionException();
        
        int k = m1._columns, n = m1._rows, m = m2._columns;
        var res = new Matrix(n, m);
        for (var i = 0; i < n; i++)
        for (var j = 0; j < m; j++)
        for (var h = 0; h < k; h++)
            res[i, j] += (m1[i, h] * m2[h, j]);

        return res;
    }

    public static Matrix ScalarMultiplication(Matrix m, float scalar)
    {
        var res = new Matrix(m._rows, m._columns);
        for (var i = 0; i < m._rows; i++)
        for (var j = 0; j < m._columns; j++)
            res[i, j] = m[i, j] * scalar;

        return res;
    }

    public static Matrix operator +(Matrix m1, Matrix m2) => Addition(m1, m2);

    public static Matrix operator -(Matrix m1, Matrix m2) => m1 + ((-1) * m2);

    public static Matrix operator *(Matrix m1, Matrix m2) => MatrixMultiplication(m1, m2);

    public static Matrix operator *(Matrix m, float scalar) => ScalarMultiplication(m, scalar);

    public static Matrix operator *(float scalar, Matrix m) => ScalarMultiplication(m, scalar);

    public static Matrix operator /(Matrix m, float scalar)
    {
        if (scalar == 0f)
            throw new DivideByZeroException();
        
        return ScalarMultiplication(m, 1 / scalar);
    }

    public static Matrix operator /(Matrix m1, Matrix m2) => m1 * m2.Inverse();

    public static Matrix operator ~(Matrix m) => m.Inverse();

    public override string ToString()
    {   
        var sb = new StringBuilder();
        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _columns; j++)
            {
                sb.Append(this[i, j] + " ");
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }

    public override bool Equals(Object? obj)
    {
        if (obj == null)
            throw new NullReferenceException();
        
        if (!this.GetType().Equals(obj.GetType()))
            throw new EngineException.DifferentTypesException();
        
        Matrix m = (Matrix)obj;
        
        if (_rows != m.Size.Item1 || _columns != m.Size.Item2)
            return false;

        for (var i = 0; i < _rows; i++)
            for (var j = 0; j < _columns; j++)
                if (this[i, j] != m[i, j])
                    return false;

        return true;
    }
}