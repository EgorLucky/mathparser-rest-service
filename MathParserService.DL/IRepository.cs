using MathParserService.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public interface IRepository
    {
        Task<List<Expression>> GetLastAsync(int limit);
        Task SaveAsync(Expression expression);
    }
}