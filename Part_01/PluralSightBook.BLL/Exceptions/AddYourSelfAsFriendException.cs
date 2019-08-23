using System;
using System.Runtime.Serialization;

namespace PluralSightBook.BLL.Exceptions
{
    public class AddYourSelfAsFriendException : Exception
    {
        public AddYourSelfAsFriendException()
        {
        }

        public AddYourSelfAsFriendException(string message) : base(message)
        {
        }

        public AddYourSelfAsFriendException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddYourSelfAsFriendException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}