using Microsoft.EntityFrameworkCore;
using System;

namespace MathParserService.DAL
{
    public class MathParserContext : DbContext
    {
        public MathParserContext(DbContextOptions<MathParserContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Математические выражения
        /// </summary>
        public DbSet<Expression> Expressions { get; set; }

        /// <summary>
        /// Параметры (переменнные) для выражений
        /// </summary>
        public DbSet<Parameter> Parameters { get; set; }

        /// <summary>
        /// Точки, в которых вычичляются значения выражений
        /// </summary>
        public DbSet<Point> Points { get; set; }

        /// <summary>
        /// Значения параметров выражений
        /// </summary>
        public DbSet<ParameterValue> ParameterValues { get; set; }
    }
}
