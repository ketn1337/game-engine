using GraphicsMath;

namespace Engine;

public class HyperEllipsoid : GameObject
{
    protected readonly List<float> SemiAxes;
    public HyperEllipsoid(Game game, Point position, Vector dir, List<float> semiAxes) : base(game, position, dir)
    {
        SemiAxes = semiAxes;
    }

    public override float? IntersectionDistance(Ray ray) //in 3d
    {
        var (x0, y0, z0) = (((Point)this["pos"])[0], ((Point)this["pos"])[1], ((Point)this["pos"])[2]);
        
        var (x1, y1, z1) = (ray.InitPt[0], ray.InitPt[1], ray.InitPt[2]);
        
        var (x2, y2, z2) = (ray.Dir[0] + x1, ray.Dir[1] + y1, ray.Dir[2] + z1);

        var (a, b, c) = (SemiAxes[0], SemiAxes[1], SemiAxes[2]);
        var (squaredA, squaredB, squaredC) = (a * a, b * b, c * c);
        
        var k1 = (y2 - y1) / (x2 - x1);
        var k2 = (z2 - z1) / (x2 - x1);

        var b1 = y1 - k1 * x1;
        var b2 = z1 - k2 * x1;

        var v = squaredA * squaredB * squaredC;

        var s1 = b1 - y0;
        var s2 = b2 - z0;

        var kfA = squaredB * squaredC + k1 * k1 * squaredA * squaredC + k2 * k2 * squaredA * squaredB;
        var kfB = 2 * (k1 * s1 * squaredA * squaredC + k2 * s2 * squaredA * squaredB - x0 * squaredB * squaredC);
        var kfC = squaredB * squaredC * x0 * x0 + s1 * s1 * squaredA * squaredC + s2 * s2 * squaredA * squaredB - v;

        var discriminant = kfB * kfB - 4 * kfA * kfC;

        if (discriminant < 0) return null!;

        if (discriminant == 0f)
        {
            var x = (-kfB) / (2 * kfA);
            var y = k1 * x + b1;
            var z = k2 * x + b2;
            var vec = new Vector(new[] { x - x1 , y - y1 , z - z1 });
            if (ray.Dir.ScalarProduct(vec) > 0f)
                return (float)Math.Sqrt(Math.Pow(x - x1, 2) + Math.Pow(y - y1, 2) + Math.Pow(z - z1, 2));
            else
                return null;
        }

        var xSolve1 = ((-kfB) + Math.Sqrt(discriminant)) / (2 * kfA);
        var xSolve2 = ((-kfB) - Math.Sqrt(discriminant)) / (2 * kfA);
        var ySolve1 = k1 * xSolve1 + b1;
        var ySolve2 = k1 * xSolve2 + b1;
        var zSolve1 = k2 * xSolve1 + b2;
        var zSolve2 = k2 * xSolve2 + b2;
        var lenSolve1 = Math.Sqrt(Math.Pow(xSolve1 - x1, 2) + Math.Pow(ySolve1 - y1, 2) + Math.Pow(zSolve1 - z1, 2));
        var lenSolve2 = Math.Sqrt(Math.Pow(xSolve2 - x1, 2) + Math.Pow(ySolve2 - y1, 2) + Math.Pow(zSolve2 - z1, 2));

        var vec1 = new Vector(new [] { (float)xSolve1 - x1, (float)ySolve1 - y1, (float)zSolve1 - z1});
        var vec2 = new Vector(new [] { (float)xSolve2 - x1, (float)ySolve2 - y1, (float)zSolve2 - z1});

        if (lenSolve1 < lenSolve2)
            if (ray.Dir.ScalarProduct(vec1) > 0f)
                return (float)lenSolve1;
            else return null;
        else
            if (ray.Dir.ScalarProduct(vec2) > 0f)
                return (float)lenSolve2;
            else return null;
    }
    

    public override void Rotate3D(float angleX, float angleY, float angleZ)
    {
        UpdateProp("dir", (((Vector)this["dir"]).AsMatrix() * Matrix.Rotate3D(angleX, angleY, angleZ))[0]);
    }

    public override void PlanarRotate((int, int) axis, float angle)
    {
        UpdateProp("dir", (((Vector)this["dir"]).AsMatrix() * Matrix.GetRotationMatrix(CS.Basis.Count, axis, angle))[0]);
    }
}