using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MathParser
{
    public class UnknownFunctionException : MathParserException
    {
        public UnknownFunctionException()
        {
        }

        public UnknownFunctionException(string message) : base(message)
        {
        }

        public UnknownFunctionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownFunctionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }


    }
}
