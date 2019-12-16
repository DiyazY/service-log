using System;

namespace sl.domain.Exceptions
{
    public class LabelDomainException : Exception
    {
        public LabelDomainException()
        {

        }
        public LabelDomainException(string message) : base(message)
        {

        }
        public LabelDomainException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
