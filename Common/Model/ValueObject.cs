namespace Common.Model
{
    public abstract class ValueObject
    {
        public abstract IEnumerable<object> GetAtomicValues();
    }
}
