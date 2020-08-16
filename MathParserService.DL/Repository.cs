using MathParserService.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public class Repository : IRepository
    {
        private readonly MathParserContext _context;

        public Repository(MathParserContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Expression expression)
        {
            await _context.AddAsync(expression);
            await _context.SaveChangesAsync();
        }

        public Task<List<Expression>> GetLastAsync(int limit)
        {
            return _context
                    .Expressions
                    .Include(e => e.Parameters)
                    .ThenInclude(parameter => parameter.Values)
                    .Include(e => e.Points)
                    .OrderByDescending(e => e.CreateDate)
                    .Take(limit)
                    .ToListAsync();
        }
    }
}