using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.FunctionFactories
{
    public class PowFactory : IFunctionFactory
    {
        private readonly MathParserClasses.MathParser _mathParser;

        public PowFactory(MathParserClasses.MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public bool Check(string expression)
        {
            bool result = false;
            int i = -1;
            foreach (var ch in expression)
            {
                i++;
                if (ch != '^')
                    continue;
                else if (i != 0 && i != expression.Length - 1)
                {
                    if (MathParserClasses.Check.IsBracketsAreBalanced(expression.Substring(0, i)) && 
                        MathParserClasses.Check.IsBracketsAreBalanced(expression.Substring(i + 1)))
                    {
                        result = true;
                        return result;
                    }
                }
            }
            return result;
        }

        public IFunction Create(string expression, ICollection<Variable> variables = null)
        {
            if (expression.Contains("^"))
            {
                int i = -1;
                foreach (var ch in expression)
                {
                    i++;
                    if (ch != '^')
                        continue;
                    else if (i != 0 && i != expression.Length - 1)
                    {
                        var @base = expression.Substring(0, i);
                        var log = expression.Substring(i + 1);
                        if (MathParserClasses.Check.IsBracketsAreBalanced(@base) && MathParserClasses.Check.IsBracketsAreBalanced(log))
                            return new Pow()
                            {
                                Base = _mathParser.Parse(@base, variables),
                                Log = _mathParser.Parse(log, variables)
                            };
                    }
                }
            }
            throw new UnknownFunctionException("This is not pow: " + expression);
        }
    }
}
