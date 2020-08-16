using AutoMapper;
using EgorLucky.MathParser;
using MathParserService.DAL;
using MathParserService.DL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public class MathParserService : IMathParserService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseService _dataBaseService;
        private readonly MathParser _mathParser;

        public MathParserService(IMapper mapper, IDatabaseService dataBaseService, MathParser mathParser)
        {
            _mapper = mapper;
            _dataBaseService = dataBaseService;
            _mathParser = mathParser;
        }

        public async Task<ComputeExpressionResponseModel> ComputeExpression(ComputeExpressionRequestModel request)
        {
            if (request.Parameters.Count > 5)
                return new ComputeExpressionResponseModel
                {
                    IsSuccessfulComputed = false,
                    ErrorMessage = "Number of parameters is bigger than 5"
                };

            var variables = request.Parameters.Select(p => p.GetVariable()).ToList();
            var parseResult = _mathParser.TryParse(request.Expression, variables);

            if (parseResult.IsSuccessfulCreated == false)
                return new ComputeExpressionResponseModel
                {
                    IsSuccessfulComputed = false,
                    ErrorMessage = parseResult.ErrorMessage
                };

            var computeResult = parseResult.Expression.ComputeValue(request.Parameters);

            //save to db
            await SaveAsync(parseResult, variables, request.Parameters, computeResult);

            return new ComputeExpressionResponseModel
            {
                IsSuccessfulComputed = true,
                Result = computeResult,
                Expression = parseResult.Expression
            };
        }

        //эта функция ничего не пишет в базу, так как в ней мало места,
        //а данных для сохранения у этой функции больше, чем у ComputeExpression
        public async Task<ComputeFunctionValuesResponseModel> ComputeFunctionValues(ComputeFunctionRequestModel request)
        {
            //validate
            var functionDimensionCount = request.ParametersTable.FirstOrDefault()?.Count;
            if (functionDimensionCount == null)
                return new ComputeFunctionValuesResponseModel
                {
                    IsSuccessfulComputed = false,
                    ErrorMessage = "Couldn't count function's dimensions"
                };

            if (request.ParametersTable.Any(p => p.Count != functionDimensionCount))
                return new ComputeFunctionValuesResponseModel
                {
                    IsSuccessfulComputed = false,
                    ErrorMessage = "Not equal parameters number for each point"
                };
            //get variables
            var variables = request.ParametersTable
                                    .SelectMany(p => p)
                                    .Select(p => p.GetVariable())
                                    .Distinct(new VariableEqualityComparer())
                                    .ToList();

            if (variables.Count != functionDimensionCount)
                return new ComputeFunctionValuesResponseModel
                {
                    IsSuccessfulComputed = false,
                    ErrorMessage = "Not equal parameters number for each point"
                };

            //parse
            var parseResult = _mathParser.TryParse(request.Expression, variables);

            if (!parseResult.IsSuccessfulCreated)
                return new ComputeFunctionValuesResponseModel
                {
                    IsSuccessfulComputed = false,
                    ErrorMessage = parseResult.ErrorMessage
                };

            var parsedExpression = parseResult.Expression;

            //compute
            var result = request.ParametersTable
                                .Select(parameters => new ComputeFunctionValueResult
                                {
                                    Value = parsedExpression.ComputeValue(parameters),
                                    Parameters = parameters
                                })
                                .ToList();

            return new ComputeFunctionValuesResponseModel
            {
                IsSuccessfulComputed = true,
                Result = result,
                Expression = parsedExpression
            };
        }

        public Task<List<Expression>> GetLastAsync(int limit)
        {
            return _dataBaseService.GetLastAsync(limit);
        }

        async Task<Expression> SaveAsync(MathTryParseResult tryParseResult,
                                            List<Variable> variables,
                                            List<EgorLucky.MathParser.Parameter> parameters,
                                            double result)
        {
            var expression = _mapper.Map<Expression>(tryParseResult);
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

            await _dataBaseService.SaveAsync(expression);

            return expression;
        }
    }
}
