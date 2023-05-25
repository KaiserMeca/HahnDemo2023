namespace Infrastructure.Exceptions
{
    public class RepeatedIdException : Exception
    {
        public RepeatedIdException()
        {
        }

        public RepeatedIdException(string message)
            : base(message)
        {
        }

        public RepeatedIdException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
