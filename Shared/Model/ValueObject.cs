namespace Shared.Model
{
    public abstract class ValueObject
    {
        public abstract IEnumerable<object> GetAtomicValues();

        public static bool operator ==(ValueObject one, ValueObject two)
        {
            return one?.Equals(two) ?? false;
        }

        public static bool operator !=(ValueObject one, ValueObject two)
        {
            return !(one?.Equals(two) ?? false);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = obj as ValueObject;
            IEnumerator<object> thisValue = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValue = other.GetAtomicValues().GetEnumerator();

            while (thisValue.MoveNext() && otherValue.MoveNext())
            {
                if (ReferenceEquals(thisValue.Current, null) != ReferenceEquals(otherValue.Current, null))
                {
                    return false;
                }

                if (thisValue.Current != null && !thisValue.Current.Equals(otherValue.Current))
                {
                    return false;
                }
            }

            return !thisValue.MoveNext() && !otherValue.MoveNext();
        }
    }
}
