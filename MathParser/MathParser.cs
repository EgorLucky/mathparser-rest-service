using MathParser;
using MathParser.Constants;
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
        public readonly List<IConst> _constants;
        public readonly SumFactory _sumFactory;
        public readonly NumberFactory _numberFactory;

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
            _numberFactory = new NumberFactory();
            _functionFactories.Add(_numberFactory);

            _constants = new List<IConst>();
            _constants.Add(new PI());
            _constants.Add(new E());
        }

        /// <summary>
        /// Парсинг математического выражения
        /// </summary>
        /// <param name="mathExpression"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        public IFunction Parse(string mathExpression, ICollection<Variable> variables)
        {
            //форматирование строки
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
            mathExpression = mathExpression.Replace(" ", "")
                                           .Replace('.', ',');
            if(!mathExpression.Contains("+-1*"))
                mathExpression = mathExpression.Replace("-", "+-1*");

            var matchedName = variables.Where(v => _constants.Select(c => c.Name.ToString()).Contains(v.Name.ToLower()))
                                        .Select(v => v.Name)
                                        .FirstOrDefault();

            if (!string.IsNullOrEmpty(matchedName))
                throw new VariableWrongNameException($"Wrong name for variable {matchedName}. There already const with the same name");


            mathExpression = mathExpression.ToLower();

            if (!Check.IsBracketsAreBalanced(mathExpression)) 
                throw new MathParserFormatException("brackets are not balanced");
                
            //начало парсинга
            var result = _sumFactory.Create(mathExpression, variables);
            
            return result;
        }

        /// <summary>
        /// Добавление фабрики сторонней реализации IFunction 
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public MathParser AddFunctionFactory(IFunctionFactory factory)
        {
            _functionFactories.Add(factory);
            return this;
        }

        /// <summary>
        /// Добавление сторонней реализации IConst 
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public MathParser AddConst(IConst constant)
        {
            _constants.Add(constant);
            return this;
        }

        /// <summary>
        /// Процесс распознавания элемента в строке и его парсинг
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        internal IFunction ParseFunction(string expression, ICollection<Variable> variables)
        {
            foreach(var factory in _functionFactories)
                if (factory.Check(expression))
                    return factory.Create(expression, variables);

            foreach (var constant in _constants)
                if (constant.Name.ToLower() == expression)
                    return _numberFactory.Create(constant.Value);

            if (variables.Any(p => p.Name.ToLower() == expression))
                return ParseVariable(expression, variables);

            throw new UnknownFunctionException("Unknown function in expression: " + expression);
        }

        IFunction ParseVariable(string expression, ICollection<Variable> variables)
        {
            var parameter = variables.Where(p => p.Name.ToLower() == expression).FirstOrDefault();
            if (parameter != null)
            {
                return parameter;
            }
            throw new UnknownVariableException("This is not a defined variable in expression: " + expression);
        }
    }
}
