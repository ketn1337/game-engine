using System.Diagnostics.CodeAnalysis;
using GraphicsMath;

namespace Engine;

public class GameCamera : GameObject
{
    private bool _lookAt = false;

    public GameCamera(Game game, Point pos, Vector dir, float fov, float drawDistance) : base(game, pos, dir)
    {
        this["fov"] = fov;
        this["drawDist"] = drawDistance;
    }

    public GameCamera(Game game, Point pos, Vector dir, float fov, float vFov, float drawDistance) : base(game, pos,
        dir)
    {
        this["fov"] = fov;
        this["vFov"] = vFov;
        this["drawDist"] = drawDistance;
    }

    public GameCamera(Game game, Point pos, float fov, Point lookAt, float drawDistance) : base(game, pos,
        new Vector(game.CS.Basis.Count))
    {
        _lookAt = true;
        this["fov"] = fov;
        this["drawDist"] = drawDistance;
        RemoveProp("dir");
        this["lookAt"] = lookAt;
    }

    public GameCamera(Game game, Point pos, float fov, float vFov, Point lookAt, float drawDistance) : base(game, pos,
        new Vector(game.CS.Basis.Count))
    {
        _lookAt = true;
        this["fov"] = fov;
        this["vFov"] = vFov;
        this["drawDist"] = drawDistance;
        RemoveProp("dir");
        this["lookAt"] = lookAt;
    }

    public Ray[,] GetRays(int n, int m)
    {
        var rayMatrix = new Ray[n, m];
        Vector dir;
        
        if (_lookAt)
            dir = CS.Basis.AsVector((Point)this["lookAt"]) - CS.Basis.AsVector((Point)this["pos"]);
        else
            dir = (Vector)this["dir"];

        var dHor = (float)this["fov"] / (n - 1);
        var dVert = (float)this["vFov"] / (m - 1);

        for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
            {
                var hor = -(i * dHor - (float)this["fov"] / 2);
                var vert = j * dVert - (float)this["vFov"] / 2;

                Ray tmp = new(CS, (Point)this["pos"], dir);
                tmp.Normalize();
                tmp.Dir = (tmp.Dir.AsMatrix() * Matrix.Rotate3D(0, vert, hor))[0];
                var len = CS.Basis.Length(dir);
                tmp.Dir *= len * len / CS.Basis.ScalarProduct(tmp.Dir, dir);
                rayMatrix[i, j] = tmp;
            }


        return rayMatrix;
    }
    
    public override void Rotate3D(float angleX, float angleY, float angleZ)
    {
        UpdateProp("dir", (((Vector)this["dir"]).AsMatrix() * Matrix.Rotate3D(angleX, angleY, angleZ))[0]);
    }
}