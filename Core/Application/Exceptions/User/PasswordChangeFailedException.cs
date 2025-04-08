using System.Runtime.Serialization;

namespace Application.Exceptions.User
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException() : base("Some thing went wrong, please try again later")
        {
        }

        public PasswordChangeFailedException(string? message) : base(message)
        {
        }

        public PasswordChangeFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
