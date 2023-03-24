using System.Text;

namespace engine;

public class Matrix
{
    private readonly int _n, _m;
    protected readonly List<List<float>> Data = new List<List<float>>{};
    public Matrix(int n, int m)
    {
        _n = n;
        _m = m;
        for (var j = 0; j < _n; j++)
        {
            var line = new List<float>(_m);
            for (var i = 0; i < _m; i++) line.Add(0);
            Data.Add(line);
        }
    }
    
    public Matrix(int n, int m, List<List<float>> matrix) : this(n, m)
    {
        for (var i = 0; i < _n; i++)
        {
            for (var j = 0; j < _m; j++)
            {
                Data[i][j] = matrix[i][j];
            }
        }
    }
    
    public (int, int) Size => (_n, _m);

    public float this[int i, int j]
    {
        get => Data[i][j];
        set => Data[i][j] = value;
    }
    public List<float> this[int i]
    {
        get => Data[i];
        set => Data[i] = value;
    }

    public static Matrix operator +(Matrix m1, Matrix m2)
    {
        if (m1._n != m2._n || m1._m != m2._m)
            throw new Exception("Matrices have different size");
        
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
        if (m1._n != m2._n || m1._m != m2._m)
            throw new Exception("Matrices have different size");
        
        var n = m1._n;
        var m = m1._m;
        var res = new Matrix(n, m);
        for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
                res[i, j] = m1[i, j] - m2[i, j];
        
        return res;
    }

    public static Matrix operator *(Matrix m1, Matrix m2)
    {
        if (m1._m != m2._n)
            throw new Exception("Amount of lines of Matrix1 and columns of Matrix2 is different");
        
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
        {
            for (var j = 0; j < m._m; j++)
            {
                res[i, j] = m[i, j] * num;
            }
        }

        return res;
    }
    
    public static Matrix operator *(float num, Matrix m)
    {
        var res = new Matrix(m._n, m._m);
        for (var i = 0; i < m._n; i++)
        {
            for (var j = 0; j < m._m; j++)
            {
                res[i, j] = m[i, j] * num;
            }
        }

        return res;
    }
    
    public static Matrix operator /(Matrix m, float num)
    {
        var res = new Matrix(m._n, m._m);
        for (var i = 0; i < m._n; i++)
        {
            for (var j = 0; j < m._m; j++)
            {
                res[i, j] = m[i, j] / num;
            }
        }

        return res;
    }
    

    private Matrix DeleteColumn(int num)
    {
        var res = new Matrix(_n, _m - 1);
        for (var i = 0; i < _n; i++)
        {
            for (var j = 0; j < _m; j++)
            {
                if (j < num) res[i, j] = this[i, j];
                //else if (j == num) continue;
                else if (j > num) res[i, j - 1] = this[i, j];
            }
        }
        
        return res;
    }
    
    private Matrix DeleteLine(int num)
    {
        var res = new Matrix(_n - 1, _m);
        for (var i = 0; i < _n; i++)
        {
            if (i == num) continue;
            for (var j = 0; j < _m; j++)
            {
                if (i < num) res[i, j] = this[i, j];
                else if (i > num) res[i - 1, j] = this[i, j];
            }
        }
        
        return res;
    }
    
    public double Determinant()
    {
        if (_n != _m)
            throw new Exception("Matrix is not square");
        
        switch (_n)
        {
            case 1:
                return this[0, 0];
                break;
            case 2:
                return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
                break;
            case 3:
                return this[0, 0] * this[1, 1] * this[2, 2] + 
                       this[0, 1] * this[1, 2] * this[2, 0] +
                       this[0, 2] * this[1, 0] * this[2, 1] -
                       this[0, 2] * this[1, 1] * this[2, 0] -
                       this[0, 1] * this[1, 0] * this[2, 2] -
                       this[0, 0] * this[1, 2] * this[2, 1];
                       
                break;
        }

        double res = 0;
        for (var i = 0; i < _n; i++)
        {
            res += Math.Pow(-1, i) * this[0, i] * DeleteColumn(i).DeleteLine(0).Determinant();
        }

        return res;
    }

    public Matrix Transpose()
    {
        var res = new Matrix(_m, _n);
        for (var i = 0; i < _m; i++)
        {
            for (var j = 0; j < _n; j++)
            {
                res[j, i] = this[i, j];
            }
        }

        return res;
    }
    
    public Matrix Inverse()
    {
        if (Determinant() == 0)
            throw new Exception("Determinant of matrix is zero");

        var vzMatrix = new Matrix(_n, _m);
        for (var i = 0; i < _n; i++)
        {
            for (var j = 0; j < _m; j++)
            {
                vzMatrix[i, j] = (float) Math.Pow(-1, i + j) * (float) DeleteColumn(j).DeleteLine(i).Determinant();
            }
        }

        return (float) (1 / Determinant()) * vzMatrix.Transpose();
    }

    public float BiLinearForm(Matrix m, Vector v1, Vector v2)
    {
        var sum = 0f;
        for (var i = 0; i < m._n; i++)
        {
            for (var j = 0; j < m._m; j++)
            {
                sum += (m[i, j] * v1[i, 0] * v2[j, 0]);
            }
        }

        return sum;
    }

    public static Matrix Identity(int n)
    {
        var res = new Matrix(n, n);
        for (var i = 0; i < n; i++)
        {
            res[i, i] = 1;
        }

        return res;
    }

    public static Matrix Gram(params Vector[] args)
    {
        var res = new Matrix(args.Length, args.Length);

        for (var i = 0; i < args.Length; i++)
        {
            for (var j = 0; j < args.Length; j++)
            {
                res[i, j] = args[i].ScalarProduct(args[j]);
            }
        }
        
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