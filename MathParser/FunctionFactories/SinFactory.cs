using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.FunctionFactories
{
    public class SinFactory : IFunctionFactory
    {
        private readonly MathParserClasses.MathParser _mathParser;

        public SinFactory(MathParserClasses.MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public bool Check(string expression) => !string.IsNullOrEmpty(expression) && expression.StartsWith("sin");

        public IFunction Create(string expression, ICollection<Variable> variables = null)
        {
            if (expression.StartsWith("sin"))
            {
                string argString = expression.Substring(3);
                Sin result = new Sin() { Argument = _mathParser.Parse(argString, variables) };
                return result;
            }
            throw new Exception("This is not sin: " + expression);
        }
    }
}
