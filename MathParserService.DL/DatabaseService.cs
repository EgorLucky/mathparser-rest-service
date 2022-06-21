using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public class DatabaseService<DatabaseEntity> : IDatabaseService<DatabaseEntity> where DatabaseEntity: IDatabaseEntity
    {
        private readonly IRepository<DatabaseEntity> _repository;

        public DatabaseService(IRepository<DatabaseEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task SaveAsync(DatabaseEntity expression)
        {
            if (expression.Id == Guid.Empty)
                expression.Id = Guid.NewGuid();

            if (expression.CreateDate == default)
                expression.CreateDate = DateTimeOffset.Now.ToUniversalTime();

            await _repository.SaveAsync(expression);
        }

        public Task<List<DatabaseEntity>> GetLastAsync(int limit)
        {
            return _repository.GetLastAsync(limit);
        }
    }
}
