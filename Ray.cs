using GraphicsMath;

namespace Engine;

public class Ray
{
    private CoordinateSystem _cs;
    private Point _initialPt;
    private Vector _dir;

    public Ray(CoordinateSystem cs, Point initialPt, Vector dir)
    {
        _cs = cs;
        _initialPt = initialPt;
        _dir = dir;
    }
}