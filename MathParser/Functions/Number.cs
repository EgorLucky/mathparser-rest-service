using MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses.Functions
{
    public class Number : IFunction
    {
        public string Name => nameof(Number);
        public double Value { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Value;
        }
    }
}
