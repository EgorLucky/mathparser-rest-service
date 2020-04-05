using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.FunctionFactories
{
    public class TgFactory : IFunctionFactory
    {
        private readonly MathParserClasses.MathParser _mathParser;

        public TgFactory(MathParserClasses.MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public bool Check(string expression) => !string.IsNullOrEmpty(expression) && expression.StartsWith("tg");

        public IFunction Create(string expression, ICollection<Variable> variables = null)
        {
            if (expression.StartsWith("tg"))
            {
                string argString = expression.Substring(2);
                Tg result = new Tg() { Argument = _mathParser.Parse(argString, variables) };
                return result;
            }
            throw new Exception("This is not tg: " + expression);
        }
    }
}
