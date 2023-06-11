using Engine;
using GraphicsMath;

/*var data = new List<List<float>> { new() { 3f, 4f }, new() { 1f, 2f } };
var matrix = new Matrix(2, 2, data);
Console.WriteLine(matrix);

var m1 = new Matrix(4, 4);
var m2 = new Matrix(2, 2);

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

//m2[1] = new Matrix(1, 2, new() { new() {3f, 4f} });
Console.WriteLine(m2);
//m2[1] = new Matrix(1, 2, new() { new() {10f, 12f} });
Console.WriteLine(m2);

Console.WriteLine(m1.Inverse() * m1);

var vec1 = new Vector(new float[] {1, 1, 1});
Console.WriteLine(vec1 * 0.5f);

Console.WriteLine(Matrix.GetRotationMatrix_X(90));
var test = new Matrix(1, 3, new() {new() {1f, 2f, 3f}});
var g = test[0];
var expectedValueOfElementGet = new Matrix(1, 3, new() {new() {1f, 2f, 3f}});
Console.WriteLine(g);
Console.WriteLine(expectedValueOfElementGet);
var test1 = new Matrix(2, 3, new() {new() {1f, 2f, 3f}, new() {4f, 5f, 6f}});
Console.WriteLine(test.Equals(test1));
Console.WriteLine(test1.GetMatrixWithoutRowAndColumn(0,0));
var vec = new Vector(new float[] {100, 114, 144});
vec.Normalize();
Console.WriteLine(vec.Length());

var id1 = new Identifier();
Console.WriteLine(id1.Id);
var id2 = new Identifier();
Console.WriteLine(id2.Id);
var basis = new VectorSpace(3, new List<Vector>
{
    new(new float[] { 1, 0, 0 }),
    new(new float[] { 0, 1, 0 }),
    new(new float[] { 0, 0, 1 })
});*/

/*var cs2 = new CoordinateSystem(new Point(new float[] { 0, 0, 0 }), new VectorSpace(3, new List<Vector>
{
    new (new float[] { 1, 0, 0 }),
    new (new float[] { 0, 1, 0 }),
    new (new float[] { 0, 0, 1 })
}));
var e1 = new Entity(cs2)
{
    ["s"] = "xuinya",
    ["a"] = new Point(new float[] { 1, 0, 0 })
};
var e2 = new Entity(cs2);
Console.WriteLine(e1["s"]);
Console.WriteLine(e1["a"]);
e1.RemoveProp("s");
e1["s"] = 234;
Console.WriteLine(e1["s"]);
Console.WriteLine(e1.Id);
var set1 = new HashSet<string> {"2", "2", "3"};
var set2 = new HashSet<string> {"2", "3"};
Console.WriteLine(set1.Equals(set2));
e1.RemoveProp("s");
Console.WriteLine(e1.IdSet);
var cs = new CoordinateSystem(new Point(new float[] { 0, 0, 0 }), new VectorSpace(3, new List<Vector>
{
    new (new float[] { 1, 0, 0 }),
    new (new float[] { 0, 1, 0 }),
    new (new float[] { 0, 0, 1 })
}));
var e = new Entity(cs);
var pt = new Point(3);
var dir = new Vector(new float[] { 0, 1, 0 });
var expected = new Vector(new float[] { 0, 0, -1 });
Console.WriteLine(basis.Normalize(new Vector (new [] { 10f, 2f, 1f })).Length());*/
//var el3 = new HyperEllipsoid(game, new Point(new float[] { 5, 2, -4 }), 
//    new Vector(new float[] { 1, -1, 0 }), 
//    new List<float> { 2, 2, 4});
var cs = new CoordinateSystem(new Point(new float[] { 0, 0, 0 }), 
    new VectorSpace(3, new List<Vector>
{
    new (new float[] { 1, 0, 0 }),
    new (new float[] { 0, 1, 0 }),
    new (new float[] { 0, 0, 1 })
}));
Game game = new Game(cs, new GameObjectsList(new List<GameObject>()));

var el1 = new HyperEllipsoid(game, new Point(new float[] { 5, 0, 0 }), 
    new Vector(new float[] { 1, -1, 0 }), 
    new List<float> { 2, 2, 1});
var el2 = new HyperEllipsoid(game, new Point(new float[] { 5, 4.1f, 0 }), 
    new Vector(new float[] { 1, -1, 0 }), 
    new List<float> { 2, 2, 1});

game.Run();
