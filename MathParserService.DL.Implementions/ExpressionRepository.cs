using MathParserService.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.Implementions
{
    public class ExpressionRepository : Repository<Expression>
    {

        public ExpressionRepository(MathParserContext context):base(context)
        {
        }

        public override Task<List<Expression>> GetLastAsync(int limit)
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
