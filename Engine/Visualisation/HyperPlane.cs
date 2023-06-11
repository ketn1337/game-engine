using GraphicsMath;

namespace Engine;

public class HyperPlane : GameObject
{
    public HyperPlane(Game game, Point pos, Vector dir) : base(game, pos, dir)
    {
        dir.Normalize();
    }

    public override void PlanarRotate((int, int) axis, float angle)
    {
        UpdateProp("dir", (((Vector)this["dir"]).AsMatrix() * Matrix.GetRotationMatrix(CS.Basis.Count, axis, angle))[0]);
    }

    public override void Rotate3D(float angleX, float angleY, float angleZ)
    {
        UpdateProp("dir", (((Vector)this["dir"]).AsMatrix() * Matrix.Rotate3D(angleX, angleY, angleZ))[0]);
    }

    public override float? IntersectionDistance(Ray ray)
    {
        var basis = CS.Basis;
        var fromRayPosToPlanePos = basis.AsVector((Point)this["pos"]) - basis.AsVector(ray.InitPt);
        var scalarProdOfRayDirAndPlaneDir = basis.ScalarProduct(ray.Dir, (Vector)this["dir"]);
        var scalarProductOfPlaneDirAndPositionsVector = basis.ScalarProduct((Vector)this["dir"], fromRayPosToPlanePos);

        if (Math.Abs(scalarProdOfRayDirAndPlaneDir) < 0.0001)
            if (Math.Abs(scalarProductOfPlaneDirAndPositionsVector) < 0.0001) 
                return 0;
            else 
                return null;
        
        var dist = scalarProductOfPlaneDirAndPositionsVector / scalarProdOfRayDirAndPlaneDir;

        return dist >= 0 ? dist : null;
    }
}