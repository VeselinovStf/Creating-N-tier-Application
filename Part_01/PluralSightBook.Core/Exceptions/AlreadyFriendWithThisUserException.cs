using System;
using System.Runtime.Serialization;

namespace PluralSightBook.Core.Exceptions
{
    public class AlreadyFriendWithThisUserException : Exception
    {
        public AlreadyFriendWithThisUserException()
        {
        }

        public AlreadyFriendWithThisUserException(string message) : base(message)
        {
        }

        public AlreadyFriendWithThisUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyFriendWithThisUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}