using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.FunctionFactories
{
    public class NumberFactory : IFunctionFactory
    {
        public bool Check(string expression) => !string.IsNullOrEmpty(expression) && 
                                                double.TryParse(expression, out double result);

        public IFunction Create(string expression, ICollection<Variable> variables = null)
        {
            double.TryParse(expression, out double result);
            return new Number() { Value = result };
        }
    }
}
