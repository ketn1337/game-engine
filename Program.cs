using tasks;

var dot1 = new Point(2, 2, 2);
var dot2 = new Point(1, 1, 1);
var dot3 = new Point(0, 0, 0);
dot3 = dot1 + dot2;
dot3.Print();
dot3 *= 8;
dot3.Print();
dot3 -= dot2;
dot3.Print();
dot3 /= 11;
dot3.Print();
Console.WriteLine(Point.Distance(dot1, dot2));

var vec1 = new Vector(Math.Sqrt(3), Math.Sqrt(3), Math.Sqrt(3));
var vec2 = new Vector(dot2);
Console.WriteLine(vec2.Length);
vec1.Norm();
Console.WriteLine(vec1.Length);
var vec = vec1 * vec2;
vec.Print();

Point.test();