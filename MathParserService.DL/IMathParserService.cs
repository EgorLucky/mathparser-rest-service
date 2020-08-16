using MathParserService.DAL;
using MathParserService.DL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public interface IMathParserService
    {
        Task<ComputeExpressionResponseModel> ComputeExpression(ComputeExpressionRequestModel request);
        Task<ComputeFunctionValuesResponseModel> ComputeFunctionValues(ComputeFunctionRequestModel request);
        Task<List<Expression>> GetLastAsync(int limit);
    }
}