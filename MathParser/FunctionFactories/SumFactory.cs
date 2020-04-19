using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser.FunctionFactories
{
    public class SumFactory : IFunctionFactory
    {
        private readonly ProductFactory _productFactory;

        public SumFactory(ProductFactory productFactory)
        {
            _productFactory = productFactory;
        }
        public bool Check(string expression) => MathParserClasses.Check.IsExpressionInBrackets(expression);

        public IFunction Create(string expression, ICollection<Variable> variables = null)
        {
            if (MathParserClasses.Check.IsExpressionInBrackets(expression))
                expression = expression.Remove(expression.Length - 1, 1)
                                        .Remove(0, 1);
            Sum result = new Sum();
            int balance = 0;
            string term = "";
            int counter = 0;

            foreach (var ch in expression)
            {
                counter++;
                if (ch == '(')
                    balance--;
                if (ch == ')')
                    balance++;
                if (ch == '+' && balance == 0 && counter != 1)
                {
                    result.Terms.Add(_productFactory.Create(term, variables));
                    term = "";
                    continue;
                }

                term += ch;
                if (counter == expression.Length)
                {
                    result.Terms.Add(_productFactory.Create(term, variables));
                    continue;
                }
            }

            if (result.Terms.Count == 1)
                return result.Terms.First();

            return result;
        }
    }
}
