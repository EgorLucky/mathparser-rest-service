using EgorLucky.MathParser;
using MathParserService.DL.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public class MathParserService<ExpressionType> : IMathParserService<ExpressionType>  where ExpressionType: IDatabaseEntity
    {
        private readonly IDatabaseService<ExpressionType> _dataBaseService;
        private readonly MathParser _mathParser;
        private readonly IExpressionFactory<ExpressionType> _expressionFactory;
        private readonly IExpressionToComputedExpressionMapper<ExpressionType> _computedExpressionMapper;

        public MathParserService(
            IDatabaseService<ExpressionType> dataBaseService, 
            MathParser mathParser, 
            IExpressionFactory<ExpressionType> expressionFactory,
            IExpressionToComputedExpressionMapper<ExpressionType> computedExpressionMapper)
        {
            _dataBaseService = dataBaseService;
            _mathParser = mathParser;
            _expressionFactory = expressionFactory;
            _computedExpressionMapper = computedExpressionMapper;
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

            var expressionDAL = _expressionFactory.Create(parseResult, variables, request.Parameters, computeResult);

            await _dataBaseService.SaveAsync(expressionDAL);

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

        public async Task<ComputeFunctionValuesResponseModel> Compute2DIntervalPlot(Compute2DIntervalPlotRequestModel request)
        {
            if(request.Max <= request.Min)
                return new ComputeFunctionValuesResponseModel
                {
                    IsSuccessfulComputed = false,
                    ErrorMessage = "Max is not bigger than Min"
                };

            if(Math.Abs(request.Max - request.Min) < request.Step)
                return new ComputeFunctionValuesResponseModel
                {
                    IsSuccessfulComputed = false,
                    ErrorMessage = "Step is bigger than interval between Max and Min"
                };

            if (request.Step <= 0)
                return new ComputeFunctionValuesResponseModel
                {
                    IsSuccessfulComputed = false,
                    ErrorMessage = "Step is not bigger than zero"
                };

            var computeFunctionRequest = new ComputeFunctionRequestModel()
            {
                Expression = request.Expression,
                ParametersTable = new List<List<Parameter>>()
            };

            for (var i = request.Min; i < request.Max; i += request.Step)
            {
                var point = new List<Parameter>()
                {
                    new Parameter
                    {
                        VariableName = "x",
                        Value = i
                    }
                };

                computeFunctionRequest.ParametersTable.Add(point);
            }

            computeFunctionRequest.ParametersTable.Add(new List<Parameter>()
            {
                new Parameter
                {
                    VariableName = "x",
                    Value = request.Max
                }
            });

            return await ComputeFunctionValues(computeFunctionRequest);
        }

        public async Task<List<ComputedExpression>> GetLastAsync(int limit)
        {
            var databaseObjects = await _dataBaseService.GetLastAsync(limit);

            var result = databaseObjects
                .Select(o => _computedExpressionMapper.Map(o))
                .ToList();

            return result;              
        }
    }
}
