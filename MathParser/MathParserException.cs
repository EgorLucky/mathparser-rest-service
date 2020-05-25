using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MathParser
{
    public class MathParserException : Exception
    {
        public MathParserException()
        {
        }

        public MathParserException(string message) : base(message)
        {
        }

        public MathParserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MathParserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
