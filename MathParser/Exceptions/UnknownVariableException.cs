using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MathParser
{
    public class UnknownVariableException : MathParserException
    {
        public UnknownVariableException()
        {
        }

        public UnknownVariableException(string message) : base(message)
        {
        }

        public UnknownVariableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownVariableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
