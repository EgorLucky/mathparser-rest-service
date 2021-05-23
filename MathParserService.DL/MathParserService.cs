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
            var errorMessage = MathParserServiceParametersChecker.CheckForComputeExpression(request, 5);

            if (!string.IsNullOrEmpty(errorMessage))
                return new ComputeExpressionResponseModel(false, errorMessage);

            var variables = request.Parameters.Select(p => p.GetVariable()).ToList();
            var parseResult = _mathParser.TryParse(request.Expression, variables);

            if (parseResult.IsSuccessfulCreated == false)
                return new ComputeExpressionResponseModel(false, parseResult.ErrorMessage);

            var computeResult = parseResult.Expression.ComputeValue(request.Parameters);

            var expressionDAL = _expressionFactory
                                    .Create(parseResult, 
                                            variables, 
                                            request.Parameters, 
                                            computeResult);

            await _dataBaseService.SaveAsync(expressionDAL);

            return new ComputeExpressionResponseModel(true)
            {
                Result = computeResult,
                Expression = parseResult.Expression
            };
        }

        //эта функция ничего не пишет в базу, так как в ней мало места,
        //а данных для сохранения у этой функции больше, чем у ComputeExpression
        public async Task<ComputeFunctionValuesResponseModel> ComputeFunctionValues(ComputeFunctionRequestModel request)
        {
            var functionDimensionCount = request.ParametersTable.FirstOrDefault()?.Count;
            
            var variables = request.ParametersTable
                                    .SelectMany(p => p)
                                    .Select(p => p.GetVariable())
                                    .Distinct()
                                    .ToList();

            var errorMessage = MathParserServiceParametersChecker.CheckForComputeFunctionValues(request, functionDimensionCount, variables);

            if (!string.IsNullOrEmpty(errorMessage))
                return new ComputeFunctionValuesResponseModel(false, errorMessage);

            //parse
            var parseResult = _mathParser.TryParse(request.Expression, variables);

            if (!parseResult.IsSuccessfulCreated)
                return new ComputeFunctionValuesResponseModel(false, parseResult.ErrorMessage);

            var parsedExpression = parseResult.Expression;

            //compute
            var result = request.ParametersTable
                                .Select(parameters => new ComputeFunctionValueResult
                                {
                                    Value = parsedExpression.ComputeValue(parameters),
                                    Parameters = parameters
                                })
                                .ToList();

            return new ComputeFunctionValuesResponseModel(true)
            {
                Result = result,
                Expression = parsedExpression
            };
        }

        public async Task<Compute2DIntervalPlotResponseModel> Compute2DIntervalPlot(Compute2DIntervalPlotRequestModel request)
        {
            var errorMessage = MathParserServiceParametersChecker.CheckForCompute2DIntervalPlot(request);
            
            if (!string.IsNullOrEmpty(errorMessage))
                return new Compute2DIntervalPlotResponseModel(false, errorMessage);

            var computeFunctionRequest = new ComputeFunctionRequestModel()
            {
                Expression = request.Expression,
                ParametersTable = new List<List<Parameter>>()
            };

            for (var i = request.Min; i < request.Max; i += request.Step)
            {
                computeFunctionRequest.ParametersTable.Add(new List<Parameter>()
                {
                    new Parameter("x", i)
                });
            }

            computeFunctionRequest.ParametersTable.Add(new List<Parameter>()
            {
                new Parameter("x", request.Max)
            });

            var computeResult = await ComputeFunctionValues(computeFunctionRequest);

            if (!computeResult.IsSuccessfulComputed)
                return new Compute2DIntervalPlotResponseModel(false, computeResult.ErrorMessage);

            var mappedComputeResult = computeResult
                            .Result
                            .Select(p => new Point2D 
                            { 
                                X = p.Parameters.First().Value, 
                                Y = p.Value 
                            })
                            .ToList();

            return new Compute2DIntervalPlotResponseModel(true)
            {
                Result = mappedComputeResult,
                Expression = computeResult.Expression
            };
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
