namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Exception thrown when a repeated ID is encountered
    /// </summary>
    public class RepeatedIdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatedIdException"/> class
        /// </summary>
        public RepeatedIdException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatedIdException"/> class with a specified error message
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        public RepeatedIdException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatedIdException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="inner">The exception that is the cause of the current exception</param>
        public RepeatedIdException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
