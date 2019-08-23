using System;
using System.Runtime.Serialization;

namespace PluralSightBook.BLL.Exceptions
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