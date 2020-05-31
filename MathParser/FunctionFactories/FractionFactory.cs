using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.FunctionFactories
{
    public class FractionFactory : IFunctionFactory
    {
        private readonly MathParserClasses.MathParser _mathParser;

        public FractionFactory(MathParserClasses.MathParser mathParser)
        {
            _mathParser = mathParser;
        }
        public bool Check(string expression)
        {
            var result = false;
            
            for(var i = expression.Length - 1; i >= 0; i--) 
            {
                var ch = expression[i];
                if (ch != '/')
                    continue;
                else if (i != 0 && i != expression.Length - 1)
                {
                    var numerator = expression.Substring(0, i);
                    var denominator = expression.Substring(i + 1);

                    if (MathParserClasses.Check.IsBracketsAreBalanced(numerator) &&
                        MathParserClasses.Check.IsBracketsAreBalanced(denominator))
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
            if (expression.Contains("/"))
            {
                for (var i = expression.Length - 1; i >= 0; i--)
                {
                    var ch = expression[i];
                    if (ch != '/')
                        continue;

                    else if (i != 0 && i != expression.Length - 1)
                    {
                        var numerator = expression.Substring(0, i);
                        var denominator = expression.Substring(i + 1);

                        if (MathParserClasses.Check.IsBracketsAreBalanced(numerator) && 
                            MathParserClasses.Check.IsBracketsAreBalanced(denominator))
                            return new Fraction()
                            {
                                Numerator = _mathParser.Parse(numerator, variables),
                                Denominator = _mathParser.Parse(denominator, variables)
                            };
                    }
                }
            }
            throw new UnknownFunctionException("This is not fraction: " + expression);
        }
    }
}
