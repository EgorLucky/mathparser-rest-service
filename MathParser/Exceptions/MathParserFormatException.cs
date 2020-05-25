using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MathParser
{
    public class MathParserFormatException : MathParserException
    {
        public MathParserFormatException()
        {
        }

        public MathParserFormatException(string message) : base(message)
        {
        }

        public MathParserFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MathParserFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
