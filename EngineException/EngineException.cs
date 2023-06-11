namespace EngineExceptions;

public abstract class EngineException
{
    public class OutOfObjectException : ApplicationException
    {
        public OutOfObjectException() : base("Index was out of object dimension") { }
    }
    
    public class ObjectDimensionException : ApplicationException
    {
        public ObjectDimensionException() : base("Object dimensions are not the right size") { }
    }
    
    public class DeterminantIsZeroException : ApplicationException
    {
        public DeterminantIsZeroException() : base("Determinant is zero") { }
    }

    public class DifferentTypesException : ApplicationException
    {
        public DifferentTypesException() : base("Objects are different types") { }
    }
    
    public class ObjectNotIncludeItem : ApplicationException
    {
        public ObjectNotIncludeItem() : base("Object isn't include this item") { }
    }
    
    public class BasisContainsLinearlyDependentVectors : ApplicationException
    {
        public BasisContainsLinearlyDependentVectors() : base("Basis contains linearly dependent vectors") { }
    }
}