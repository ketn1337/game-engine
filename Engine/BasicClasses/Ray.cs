using System.Text;
using GraphicsMath;

namespace Engine;

public class Ray
{
    private CoordinateSystem _cs;
    private Point _initPt;
    private Vector _dir;

    public Ray(CoordinateSystem cs, Point initPt, Vector dir)
    {
        _cs = cs;
        _initPt = initPt;
        _dir = dir;
    }

    public CoordinateSystem CS => _cs;
    
    public Point InitPt => _initPt;

    public Vector Dir
    {
        get => _dir;
        set => _dir = value;
    }

    public void Normalize()
    {
        var len = _dir.Length();
        _dir /= len;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < _dir.Count; i++)
            sb.Append(_dir[i] + " ");

        sb.Append('\n');
        
        return sb.ToString();
    }
}