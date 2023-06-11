using GraphicsMath;

namespace Engine;

public class GameObject : Entity
{
    public GameObject(Game game, Point pos, Vector dir) : base(game.CS)
    {
        SetProperty("pos", pos);
        SetProperty("dir", dir);
        game.Append(this);
    }

    public void Move(Vector dir)
    {
        UpdateProp("pos", (Point)this["pos"] + dir);
    }

    public virtual void PlanarRotate((int, int) axis, float angle)
    {
        
    }

    public virtual void Rotate3D(float angleX, float angleY, float angleZ)
    {
       
    }

    public void SetPos(Point pos)
    {
        this.RemoveProp("pos");
        this["pos"] = pos;
    }

    public void SetDir(Vector dir)
    {
        dir.Normalize();
        this.RemoveProp("dir");
        this["dir"] = dir;
    }

    public virtual float? IntersectionDistance(Ray ray) => null;
}