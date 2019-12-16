using System;

namespace sl.domain.Exceptions
{
    public class LogDomainException : Exception
    {
        public LogDomainException()
        {

        }
        public LogDomainException(string message) : base(message)
        {

        }
        public LogDomainException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
