using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public interface IRepository<DatabaseEntity> where DatabaseEntity: IDatabaseEntity
    {
        Task<List<DatabaseEntity>> GetLastAsync(int limit);
        Task SaveAsync(DatabaseEntity expression);
    }
}