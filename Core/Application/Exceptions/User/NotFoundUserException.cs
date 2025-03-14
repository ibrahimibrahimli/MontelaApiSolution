using System.Runtime.Serialization;

namespace Application.Exceptions.User
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException(): base("Invalid username or password")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
