using System;
using System.Runtime.Serialization;

namespace PluralSightBook.BLL.Exceptions
{
    public class NoSuchUserException : Exception
    {
        public NoSuchUserException()
        {
        }

        public NoSuchUserException(string message) : base(message)
        {
        }

        public NoSuchUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}