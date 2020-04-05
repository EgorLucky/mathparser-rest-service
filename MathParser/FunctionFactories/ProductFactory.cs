using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MathParser.FunctionFactories
{
    public class ProductFactory: IFunctionFactory
    {
        private readonly MathParserClasses.MathParser _mathParser;

        public ProductFactory(MathParserClasses.MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public bool Check(string expression) => MathParserClasses.Check.IsExpressionInBrackets(expression);

        public IFunction Create(string expression, ICollection<Variable> variables = null)
        {
            if (MathParserClasses.Check.IsExpressionInBrackets(expression))
                expression = expression.Remove(expression.Length - 1, 1)
                                        .Remove(0, 1);

            Product result = new Product();
            int balance = 0;
            string factor = "";
            int counter = 0;

            foreach (var ch in expression)
            {
                counter++;
                if (ch == '(')
                    balance--;
                if (ch == ')')
                    balance++;

                if (ch == '*' && balance == 0)
                {
                    result.Factors.Add(_mathParser.ParseFunction(factor, variables));
                    factor = "";
                    continue;
                }

                factor += ch;
                if (counter == expression.Length)
                {
                    result.Factors.Add(_mathParser.ParseFunction(factor, variables));
                    continue;
                }
            }

            if (result.Factors.Count == 1)
                return result.Factors.First();

            return result;
        }
    }
}