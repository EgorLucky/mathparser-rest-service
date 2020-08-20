using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MathParserService.DAL
{
    /// <summary>
    /// Математическое выражение
    /// </summary>
    public class Expression: DL.IDatabaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Строковое представление выражения
        /// </summary>
        public string ExpressionString { get; set; }

        /// <summary>
        /// Выражение в формате JSON
        /// </summary>
        [Column(TypeName = "jsonb")]
        public string ExpressionJson { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// Точки, в которых были вычислены значения выражения
        /// </summary>
        public List<Point> Points { get; set; }

        /// <summary>
        /// Параметры(переменные) выражения
        /// </summary>
        public List<Parameter> Parameters { get; set; }
    }
}