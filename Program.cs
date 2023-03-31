using engine;

var list1 = new List<float>{4f, 5f};
var list2 = new List<float>{1f, 2f};
var data = new List<List<float>>{new List<float> {3, 4}, list2};
Console.WriteLine(data[0][0] + " " + data[0][1] + " " + data[1][0] + " " + data[1][1] + "\n");
var matrix = new Matrix(2, 2, data);

var n = 2;
var m = 2;
var m1 = new Matrix(4, 4);
var m2 = new Matrix(n, m);

m1[0, 0] = 159;
m1[0, 1] = 264;
m1[0, 2] = 44;
m1[0, 3] = 24;
m1[1, 0] = 138;
m1[1, 1] = 227;
m1[1, 2] = 36;
m1[1, 3] = 1;
m1[2, 0] = 202;
m1[2, 1] = 311;
m1[2, 2] = 122;
m1[2, 3] = 23;
m1[3, 0] = 14;
m1[3, 1] = 241;
m1[3, 2] = 141;
m1[3, 3] = 54;

m2[0, 0] = 3;
m2[0, 1] = 1;
m2[1, 0] = 2;
m2[1, 1] = 1;
Console.WriteLine(m1.Inverse() * m1);
float[] one = { 3, 1, 1 };
float[] two = { 2, 1, 2 };
var v1 = new Vector(one);
var v2 = new Vector(two);
Console.WriteLine(v1.VectorProduct(v2));
Console.WriteLine(m1.Determinant());