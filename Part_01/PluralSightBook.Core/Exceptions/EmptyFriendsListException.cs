using System;
using System.Runtime.Serialization;

namespace PluralSightBook.Core.Exceptions
{
    public class EmptyFriendsListException : Exception
    {
        public EmptyFriendsListException()
        {
        }

        public EmptyFriendsListException(string message) : base(message)
        {
        }

        public EmptyFriendsListException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyFriendsListException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}