using System.Runtime.Serialization;

namespace Application.Exceptions.User
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException() : base("An unexpected error was encountered while creating the user.")
        {
        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
