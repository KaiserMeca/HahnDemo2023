namespace Infrastructure.Exceptions
{
    public class EmailNotificationException : Exception
    {
        public EmailNotificationException()
        {
        }

        public EmailNotificationException(string message)
            : base(message)
        {
        }

        public EmailNotificationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
