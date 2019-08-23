using System;
using System.Runtime.Serialization;

namespace PluralSightBook.BLL.Exceptions
{
    public class NoSuchUserToRemoveException : Exception
    {
        public NoSuchUserToRemoveException()
        {
        }

        public NoSuchUserToRemoveException(string message) : base(message)
        {
        }

        public NoSuchUserToRemoveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchUserToRemoveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}