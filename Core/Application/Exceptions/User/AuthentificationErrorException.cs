using System.Runtime.Serialization;

namespace Application.Exceptions.User
{
    public class AuthentificationErrorException : Exception
    {
        public AuthentificationErrorException() : base("Authentification failed")
        {
        }

        public AuthentificationErrorException(string? message) : base(message)
        {
        }

        public AuthentificationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
