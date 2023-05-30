namespace Shared.Model
{
    /// <summary>
    /// Represents an abstract class for a value object.
    /// </summary>
    public abstract class ValueObject 
    {
        /// <summary>
        /// Gets the atomic values of the value object.
        /// </summary>
        public abstract IEnumerable<object> GetAtomicValues();

        /// <summary>
        /// Implements the equality operator for comparing two value objects.
        /// </summary>
        /// <param name="one">The first value object.</param>
        /// <param name="two">The second value object.</param>
        /// <returns>True if the two value objects are equal, false otherwise.</returns>
        public static bool operator ==(ValueObject one, ValueObject two)
        {
            return one?.Equals(two) ?? false;
        }

        /// <summary>
        /// Implements the inequality operator for comparing two value objects.
        /// </summary>
        /// <param name="one">The first value object.</param>
        /// <param name="two">The second value object.</param>
        /// <returns>True if the two value objects are not equal, false otherwise.</returns>
        public static bool operator !=(ValueObject one, ValueObject two)
        {
            return !(one?.Equals(two) ?? false);
        }

        /// <summary>
        /// Determines whether this value object is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with this value object.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject? other = obj as ValueObject;
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
