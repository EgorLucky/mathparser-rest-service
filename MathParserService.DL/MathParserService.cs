using EgorLucky.MathParser;
using MathParserService.DL.ApiModels;
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

        public MathParserService(IDatabaseService<ExpressionType> dataBaseService, MathParser mathParser, IExpressionFactory<ExpressionType> expressionFactory)
        {
            _dataBaseService = dataBaseService;
            _mathParser = mathParser;
            _expressionFactory = expressionFactory;
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

        public Task<List<ExpressionType>> GetLastAsync(int limit)
        {
            return _dataBaseService.GetLastAsync(limit);
        }
    }
}
