using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.FunctionFactories
{
    public class CosFactory : IFunctionFactory
    {
        private readonly MathParserClasses.MathParser _mathParser;

        public CosFactory(MathParserClasses.MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public bool Check(string expression) => !string.IsNullOrEmpty(expression) && 
                                                expression.StartsWith("cos");

        public IFunction Create(string expression, ICollection<Variable> variables = null)
        {
            if (expression.StartsWith("cos"))
            {
                string argString = expression.Substring(3);
                Cos result = new Cos() { Argument = _mathParser.Parse(argString, variables) };
                return result;
            }
            throw new UnknownFunctionException("This is not cos: " + expression);
        }
    }
}
