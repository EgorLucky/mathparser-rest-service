using AutoMapper;
using EgorLucky.MathParser;
using MathParserService.DAL;
using MathParserService.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParserService.DL.Implementions
{
    public class ExpressionFactory : IExpressionFactory<Expression>
    {
        private readonly IMapper _mapper;

        public ExpressionFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Expression Create(MathTryParseResult parseResult, List<Variable> variables, List<EgorLucky.MathParser.Parameter> parameters, double result)
        {
            var expression = _mapper.Map<Expression>(parseResult);
            //data access layer parameter
            var dalParameters = _mapper.Map<List<DAL.Parameter>>(variables);

            var point = new Point
            {
                Result = result,
                Expression = expression
            };

            expression.Parameters = new List<DAL.Parameter>();
            expression.Parameters.AddRange(dalParameters);

            expression.Points = new List<Point>();
            expression.Points.Add(point);

            foreach (var param in parameters)
            {
                var parameterValue = new ParameterValue
                {
                    Point = point,
                    Parameter = dalParameters
                                    .Where(p => p.Name == param.VariableName)
                                    .FirstOrDefault(),
                    Value = param.Value
                };

                parameterValue.Parameter.Values = new List<ParameterValue>();
                parameterValue.Parameter.Values.Add(parameterValue);

                point.Coordinates = new List<ParameterValue>();
                point.Coordinates.Add(parameterValue);
            }

            return expression;
        }
    }
}
