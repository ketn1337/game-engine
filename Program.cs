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

// тесты для перегрузок и Lenght через Distance