namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Exception thrown when an error occurs during email notification
    /// </summary>
    public class EmailNotificationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationException"/> class
        /// </summary>
        public EmailNotificationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationException"/> class with a specified error message
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        public EmailNotificationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="inner">The exception that is the cause of the current exception</param>
        public EmailNotificationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
