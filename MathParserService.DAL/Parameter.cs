using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathParserService.DAL
{
    /// <summary>
    /// Параметр (переменная) от математического выражения
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Имя параметра
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор математического выражения, к которому относится параметр
        /// </summary>
        public Guid ExpressionId { get; set; }

        /// <summary>
        /// Математическое выражение, к которому относится параметр
        /// </summary>
        public Expression Expression { get; set; }

        /// <summary>
        /// Значения параметра
        /// </summary>
        public List<ParameterValue> Values { get; set; }
    }
}