using engine;

Console.WriteLine("Class Point tests");

var dot1 = new Point(2, 2, 2);
var dot2 = new Point(1, 1, 1);
var dot3 = new Point(0, 0, 0);

Console.WriteLine("Distance from point (0, 0, 0) to point (1, 1, 1) test. If u see 1,73... (sqrt(3)) than its ok : " + dot3.Distance(dot2));

Console.Write("(1, 1, 1) + (2, 2, 2) test. If u see (3, 3, 3) than its ok : ");
dot3 = dot1 + dot2;
dot3.Print();

Console.Write("(3, 3, 3) * 5 test. If u see (15, 15, 15) than its ok : ");
dot3 = dot3 * 5;
dot3.Print();

Console.Write("2 * (15, 15, 15) test. If u see (30, 30, 30) than its ok : ");
dot3 = 2 * dot3;
dot3.Print();

Console.Write("(30, 30, 30) - (2, 2, 2) test. If u see (28, 28, 28) than its ok : ");
dot3 = dot3 - dot1;
dot3.Print();

Console.Write("(28, 28, 28) / 14 test. If u see (2, 2, 2) than its ok : ");
dot3 = dot3 / 14;
dot3.Print();

Console.WriteLine("---------------------------------------------------------------");


Console.WriteLine("Class Vector tests");

var vec1 = new Vector(1, 1, 1);
var vec2 = new Vector(dot2);

Console.Write("Length of vector {1, 1, 1} test. If u see 1,73... (sqrt(3)) than its ok: ");
Console.WriteLine(vec2.Length);

Console.Write("Length of normal vector {1, 1, 1} test. If u see 1 than its ok: ");
vec1.Norm();
Console.WriteLine(vec1.Length);

var list1 = new List<float>{4f, 5f};
var list2 = new List<float>{1f, 2f};
var data = new List<List<float>>{new List<float> {3, 4}, list2};
Console.WriteLine(data[0][0] + " " + data[0][1] + " " + data[1][0] + " " + data[1][1]);
var matrix = new Matrix(2, 2, data);

var n = 2;
var m = 2;
var m1 = new Matrix(n, m);
var m2 = new Matrix(n, m);
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        m1[i, j] = 1;
        m2[i, j] = 1;
    }
}
var res = new Matrix(n, m);
/*res = m1 + m2;
res.Print();
res = res - m1;
res.Print();*/
res = m1 * m2;
Console.Write(res.ToString());