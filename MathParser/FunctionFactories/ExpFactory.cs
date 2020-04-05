using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.FunctionFactories
{
    public class ExpFactory : IFunctionFactory
    {
        private readonly MathParserClasses.MathParser _mathParser;

        public ExpFactory(MathParserClasses.MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public bool Check(string expression) => !string.IsNullOrEmpty(expression) && expression.StartsWith("exp");

        public IFunction Create(string expression, ICollection<Variable> variables = null)
        {
            if (expression.StartsWith("exp"))
            {
                string argString = expression.Substring(3);
                Exp result = new Exp() { Argument = _mathParser.Parse(argString, variables) };
                return result;
            }
            throw new Exception("This is not exp: " + expression);
        }


    }
}
