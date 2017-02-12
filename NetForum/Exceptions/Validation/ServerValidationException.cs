using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions.Validation
{
    public class ServerValidationException : Exception
    {
        public enum ServerValidationExceptionType
        {
            Success,
            Error
        };

        public ServerValidationExceptionType ValidationExceptionType { get; set; }

        public ServerValidationException()
        {

        }

        public ServerValidationException(string message) : base(message)
        {

        }

        public ServerValidationException(string message, ServerValidationExceptionType exceptionType) : base(message)
        {
            ValidationExceptionType = exceptionType;
        }

        public ServerValidationException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
