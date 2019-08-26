using System;
using System.Runtime.Serialization;

namespace PluralSightBook.Core.Exceptions
{
    public class StringParameterException : Exception
    {
        public StringParameterException()
        {
        }

        public StringParameterException(string message) : base(message)
        {
        }

        public StringParameterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StringParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}