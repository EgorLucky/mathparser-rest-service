using MathParserService.DAL;
using MathParserService.DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathParserService.DL.Implementions
{
    public class Repository<DatabaseEntity> : IRepository<DatabaseEntity> where DatabaseEntity : class, IDatabaseEntity
    {
        protected readonly MathParserContext _context;

        public Repository(MathParserContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(DatabaseEntity expression)
        {
            await _context.AddAsync(expression);
            await _context.SaveChangesAsync();
        }

        public virtual Task<List<DatabaseEntity>> GetLastAsync(int limit)
        {
            return _context
                    .Set<DatabaseEntity>()
                    .OrderByDescending(e => e.CreateDate)
                    .Take(limit)
                    .ToListAsync();
        }
    }
}