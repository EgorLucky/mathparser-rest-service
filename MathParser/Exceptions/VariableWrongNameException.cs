using MathParser;
using System;
using System.Runtime.Serialization;

namespace MathParserClasses
{
    public class VariableWrongNameException : MathParserException
    {
        public VariableWrongNameException()
        {
        }

        public VariableWrongNameException(string message) : base(message)
        {
        }

        public VariableWrongNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VariableWrongNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}