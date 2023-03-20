using System.Text;

namespace engine;

public class Matrix
{
    private int _n, _m;
    private List<List<float>> _matrix = new List<List<float>>{};
    public Matrix(int n, int m)
    {
        _n = n;
        _m = m;
        for (var j = 0; j < _n; j++)
        {
            var line = new List<float>(_m);
            for (var i = 0; i < _m; i++) line.Add(0);
            _matrix.Add(line);
        }
    }

    public Matrix(int n, int m, List<List<float>> matrix) : this(n, m)
    {
        for (var i = 0; i < _n; i++)
        {
            for (var j = 0; j < _m; j++)
            {
                _matrix[i][j] = matrix[i][j];
            }
        }
    }
    
    public (int, int) Size => (_n, _m);

    public float this[int i, int j]
    {
        get => _matrix[i][j];
        set => _matrix[i][j] = value;
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

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < _n; i++){
            for (var j = 0; j < _m; j++)
            {
                sb.Append(_matrix[i][j] + " ");
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }
}