using MathParserService.DL.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public interface IMathParserService<ExpressionType> where ExpressionType: IDatabaseEntity
    {
        Task<ComputeExpressionResponseModel> ComputeExpression(ComputeExpressionRequestModel request);
        Task<ComputeFunctionValuesResponseModel> ComputeFunctionValues(ComputeFunctionRequestModel request);
        Task<List<ComputedExpression>> GetLastAsync(int limit);
    }
}