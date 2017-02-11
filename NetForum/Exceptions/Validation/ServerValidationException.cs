using System;

namespace Exceptions.Validation
{
    public class ServerValidationException : Exception
    {
        public ServerValidationException()
        {

        }

        public ServerValidationException(string message) : base(message)
        {

        }

        public ServerValidationException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
