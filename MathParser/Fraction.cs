using MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses
{
    class Fraction: IFunction
    {
        public IFunction Numerator { get; set; }
        public IFunction Denominator { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Numerator.ComputeValue(variables) / Denominator.ComputeValue(variables);
        }

    }
}
