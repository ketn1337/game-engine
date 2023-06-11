using EngineExceptions;

namespace GraphicsMath;

public class CoordinateSystem
{
    private Point _initialPt;
    private VectorSpace _basis;
    
    public CoordinateSystem(Point initialPt, VectorSpace basis)
    {
        _basis = basis;
        _initialPt = initialPt;
    }

    public Point InitialPt => _initialPt;

    public VectorSpace Basis => _basis;

    public override bool Equals(object? obj)
    {
        if (obj == null)
            throw new NullReferenceException();
        
        if (!this.GetType().Equals(obj.GetType()))
            throw new EngineException.DifferentTypesException();

        CoordinateSystem cs = (CoordinateSystem)obj;
        
        if (InitialPt.Count != cs.InitialPt.Count || Basis.Count != cs.Basis.Count)
            return false;

        return InitialPt.Equals(cs.InitialPt) && Basis.Equals(cs.Basis);
    }
}