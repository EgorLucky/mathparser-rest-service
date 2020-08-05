using System;
using System.ComponentModel.DataAnnotations;

namespace MathParserService.DAL
{
    /// <summary>
    /// Значение парамтера в точке
    /// </summary>
    public class ParameterValue
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор точки, к которой относится значение параметра
        /// </summary>
        public Guid PointId { get; set; }

        /// <summary>
        /// Точка, к которой относится значение параметра
        /// </summary>
        public Point Point { get; set; }

        /// <summary>
        /// Идентификатор параметра, к которому относится значение
        /// </summary>
        public Guid ParameterId { get; set; }

        /// <summary>
        /// Параметр, к которому относится значение
        /// </summary>
        public Parameter Parameter { get; set; }

        /// <summary>
        /// Значение параметра
        /// </summary>
        public double Value { get; set; }
    }
}