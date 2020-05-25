using MathParser;
using MathParser.FunctionFactories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathParserClasses
{
    public class MathParser
    {
        public readonly List<IFunctionFactory> _functionFactories;
        public readonly SumFactory _sumFactory;

        public MathParser()
        {
            _functionFactories = new List<IFunctionFactory>();

            _functionFactories.Add(new PowFactory(this));
            _functionFactories.Add(new FractionFactory(this));
            _functionFactories.Add(new SinFactory(this));
            _functionFactories.Add(new CosFactory(this));
            _functionFactories.Add(new TgFactory(this));
            _functionFactories.Add(new CtgFactory(this));
            _functionFactories.Add(new ExpFactory(this));
            _sumFactory = new SumFactory(new ProductFactory(this));
            _functionFactories.Add(_sumFactory);
            _functionFactories.Add(new NumberFactory());
        }

        public IFunction Parse(string mathExpression, ICollection<Variable> variables)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
            mathExpression = mathExpression.Replace(" ", "")
                                           .Replace('.', ',');

            if(!mathExpression.Contains("+-1*"))
                mathExpression = mathExpression.Replace("-", "+-1*");
                                            
            if (!Check.IsBracketsAreBalanced(mathExpression)) 
                throw new MathParserFormatException("brackets are not balanced");
                
            mathExpression = mathExpression.ToLower();
            
            var result = _sumFactory.Create(mathExpression, variables);
            
            return result;
        }

        public MathParser AddFunctionFactory(IFunctionFactory factory)
        {
            _functionFactories.Add(factory);
            return this;
        }
        
        internal IFunction ParseFunction(string expression, ICollection<Variable> variables)
        {
            foreach(var factory in _functionFactories)
                if (factory.Check(expression))
                    return factory.Create(expression, variables);

            if (variables.Any(p => p.Name == expression))
                return ParseVariable(expression, variables);

            throw new UnknownFunctionException("Unknown function in expression: " + expression);
        }

        IFunction ParseVariable(string expression, ICollection<Variable> variables)
        {
            var parameter = variables.Where(p => p.Name == expression).FirstOrDefault();
            if (parameter != null)
            {
                return parameter;
            }
            throw new UnknownVariableException("This is not a defined variable in expression: " + expression);
        }
    }
}
