namespace engine;

public class CoordinateSystem
{
    private Point _initialPt;
    private VectorSpace _basis;

    public CoordinateSystem(Point initialPt, VectorSpace basis)
    {
        _basis = basis;
        _initialPt = initialPt;
    }
}