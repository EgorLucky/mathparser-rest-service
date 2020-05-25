using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.FunctionFactories
{
    public class CtgFactory : IFunctionFactory
    {
        private readonly MathParserClasses.MathParser _mathParser;

        public CtgFactory(MathParserClasses.MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public bool Check(string expression) => !string.IsNullOrEmpty(expression) && 
                                                expression.StartsWith("ctg");

        public IFunction Create(string expression, ICollection<Variable> variables = null)
        {
            if (expression.StartsWith("ctg"))
            {
                string argString = expression.Substring(3);
                Ctg result = new Ctg() { Argument = _mathParser.Parse(argString, variables) };
                return result;
            }
            throw new UnknownFunctionException("This is not ctg: " + expression);
        }
    }
}
