using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathParserService.DAL
{
    /// <summary>
    /// Точка, в которой было вычислено значение выражения
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Результат вычисления выражения
        /// </summary>
        public double Result { get; set; }
        /// <summary>
        /// Идентификатор математического выражения, к которому относится точка
        /// </summary>
        public Guid ExpressionId { get; set; }

        /// <summary>
        /// Математическое выражение, к которому относится точка
        /// </summary>
        public Expression Expression { get; set; }

        /// <summary>
        /// Координаты точки
        /// </summary>
        public List<ParameterValue> Coordinates { get; set; }
    }
}