namespace engine;

public class Matrix
{
    private static int _n, _m;
    private List<List<float>> _matrix = new List<List<float>>{};
    public Matrix(int n, int m)
    {
        _n = n;
        _m = m;
        var str = new List<float>();
        for (var i = 0; i < _m; i++) str.Add(0);
        for (var i = 0; i < _n; i++) _matrix.Add(str);
    }

    public Matrix(int n, int m, List<List<float>> matrix)
    {
        _n = n;
        _m = m;
        var str = new List<float>();
        for (var i = 0; i < _m; i++) str.Add(0);
        for (var i = 0; i < _n; i++) _matrix.Add(str);
        _matrix.InsertRange(0, matrix);
    }

    private (int, int) Size
    {
        get => (_n, _m);
    }
    public float this[int index1, int index2]
    {
        get => _matrix[index1][index2];
        set => _matrix[index1][index2] = value;
    }

    public static Matrix operator +(Matrix m1, Matrix m2)
    {
        var res = new Matrix(_n, _m);
        if (_n == _m)
        {
            for (var i = 0; i < _n; i++)
            {
                for (var j = 0; j < _m; j++)
                {
                    res[i, j] = m1[i, j] + m2[i, j];
                }
            }
        }
        else
        {
            throw new Exception("Matrices have different size");
        }
        return res;
    }
    
    public static Matrix operator -(Matrix m1, Matrix m2)
    {
        if (_n == _m)
        {
            var res = new Matrix(_n, _m);
            for (var i = 0; i < _n; i++)
            {
                for (var j = 0; j < _m; j++)
                {
                    res[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return res;
        }
        else
        {
            throw new Exception("Matrices have different size");
        }
    }

    public static Matrix operator *(Matrix m1, Matrix m2)
    {
        if (m1.Size.Item2 == m2.Size.Item1)
        {
            int k = m1.Size.Item2, n = m1.Size.Item1, m = m2.Size.Item2;
            var res = new Matrix(n, m);
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    for (var h = 0; h < k; h++)
                    {
                        res[i, j] += (m1[i, h] * m2[h, j]);
                    }
                }
            }

            return res;
        }
        else
        {
            throw new Exception("Amount of lines of Matrix1 and columns of Matrix2 is different");
        }
    }
    
    public void Print()
    {
        for (var i = 0; i < _n; i++){
            for (var j = 0; j < _m; j++)
            {
                Console.Write(_matrix[i][j] + " ");
            }
            Console.Write("\n");
        }
    }

    public static void Print(Matrix m)
    {
        for (var i = 0; i < m.Size.Item1; i++){
            for (var j = 0; j < m.Size.Item2; j++)
            {
                Console.Write(m[i, j] + " ");
            }
            Console.Write("\n");
        }
    }
}