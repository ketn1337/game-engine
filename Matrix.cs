using System.Text;

namespace engine;

public class Matrix
{
    private readonly int _n, _m;
    private readonly List<List<float>> _data = new();
    public Matrix(int n, int m)
    {
        _n = n;
        _m = m;
        for (var j = 0; j < _n; j++)
        {
            var line = new List<float>(_m);
            for (var i = 0; i < _m; i++) line.Add(0);
            _data.Add(line);
        }
    }
    
    public Matrix(int n, int m, List<List<float>> matrix) : this(n, m)
    {
        //todo exception
        for (var i = 0; i < _n; i++)
        {
            for (var j = 0; j < _m; j++)
            {
                _data[i][j] = matrix[i][j];
            }
        }
    }
    
    public (int, int) Size => (_n, _m);

    public float this[int i, int j]
    {
        get
        {
            if ((i < 0 || i > _n) || (j < 0 || j > _m))
                throw new Exception("Index out of range");
            
            return _data[i][j];
        }
        //todo exception in set
        set => _data[i][j] = value;
    }
    public List<float> this[int i]
    {
        get
        {
            if (i < 0 || i > _n)
                throw new Exception("Index out of range");
            
            //todo matrix n * 1
            return _data[i];
        }
        //todo exception in set
        set => _data[i] = value;
    }

    public static Matrix operator +(Matrix m1, Matrix m2)
    {
        if (m1._n != m2._n || m1._m != m2._m)
            throw new Exception($"Matrices have different size: {m1._n} != {m2._n} or {m1._m} != {m2._m}");
        
        var n = m1._n;
        var m = m1._m;
        var res = new Matrix(n, m);
        for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
                res[i, j] = m1[i, j] + m2[i, j];
        
        return res;
    }
    
    public static Matrix operator -(Matrix m1, Matrix m2)
    {
        return m1 + ((-1) * m2);
    }

    public static Matrix operator *(Matrix m1, Matrix m2)
    {
        if (m1._m != m2._n)
            throw new Exception($"Amount of lines of Matrix1 and columns of Matrix2 is different: {m1._m} != {m2._n}");
        
        int k = m1._m, n = m1._n, m = m2._m;
        var res = new Matrix(n, m);
        for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
                for (var h = 0; h < k; h++)
                    res[i, j] += (m1[i, h] * m2[h, j]);

        return res;
    }

    public static Matrix operator *(Matrix m, float num)
    {
        var res = new Matrix(m._n, m._m);
        for (var i = 0; i < m._n; i++)
            for (var j = 0; j < m._m; j++)
                res[i, j] = m[i, j] * num;

        return res;
    }
    
    public static Matrix operator *(float num, Matrix m)
    {
        return m * num;
    }
    
    public static Matrix operator /(Matrix m, float num)
    {
        if (num == 0f)
            throw new Exception("Division by zero");
        
        return m * (1 / num);
    }

    public static Matrix operator /(Matrix m1, Matrix m2)
    {
        return m1 * m2.Inverse();
    }
    
    //todo minor

    private Matrix GetMatrixWithoutColumn(int num)
    {
        var res = new Matrix(_n, _m - 1);
        for (var i = 0; i < _n; i++)
            for (var j = 0; j < _m; j++)
            {
                if (j < num) res[i, j] = this[i, j];
                if (j > num) res[i, j - 1] = this[i, j];
            }
        
        return res;
    }
    
    private Matrix GetMatrixWithoutRow(int num)
    {
        var res = new Matrix(_n - 1, _m);
        for (var i = 0; i < _n; i++)
        {
            if (i == num) continue;
            for (var j = 0; j < _m; j++)
            {
                if (i < num) res[i, j] = this[i, j];
                if (i > num) res[i - 1, j] = this[i, j];
            }
        }
        
        return res;
    }
    
    public double Determinant()
    {
        if (_n != _m)
            throw new Exception($"Matrix is not square: {_n} != {_m}");

        if (_n == 1)
            return this[0, 0];
        

        double res = 0;
        for (var i = 0; i < _n; i++)
        {
            var submatrix = this.GetMatrixWithoutColumn(i).GetMatrixWithoutRow(0);
            res += Math.Pow(-1, i) * this[0, i] * submatrix.Determinant();
        }

        return res;
    }

    public Matrix Transpose()
    {
        var res = new Matrix(_m, _n);
        for (var i = 0; i < _m; i++)
            for (var j = 0; j < _n; j++)
                res[j, i] = this[i, j];

        return res;
    }
    
    public Matrix Inverse()
    {
        if (Determinant() == 0.0)
            throw new Exception("Determinant of matrix is zero");

        var cofactorMatrix = new Matrix(_n, _m);
        for (var i = 0; i < _n; i++)
        for (var j = 0; j < _m; j++)
        {
            var submatrix = this.GetMatrixWithoutColumn(j).GetMatrixWithoutRow(i);
            cofactorMatrix[i, j] = (float) Math.Pow(-1, i + j) * (float) submatrix.Determinant();
        }

        return cofactorMatrix.Transpose() / (float) this.Determinant();
    }

    public float BiLinearForm(Matrix m, Vector v1, Vector v2)
    {
        var sum = 0f;
        for (var i = 0; i < m._n; i++)
            for (var j = 0; j < m._m; j++)
                sum += (m[i, j] * v1[i, 0] * v2[j, 0]);

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
        var res = new Matrix(args.Length, args.Length);

        for (var i = 0; i < args.Length; i++)
            for (var j = 0; j < args.Length; j++)
                res[i, j] = args[i].ScalarProduct(args[j]);
        
        return res;
    }

    public override string ToString()
    {   
        var sb = new StringBuilder();
        for (var i = 0; i < _n; i++){
            for (var j = 0; j < _m; j++)
            {
                sb.Append(this[i, j] + " ");
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }
}