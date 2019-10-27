using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses
{
    class Fraction:IFunction
    {
        public IFunction Numerator { get; set; }
        public IFunction Denominator { get; set; }
        public double ComputeValue()
        {
            return Numerator.ComputeValue() / Denominator.ComputeValue();
        }

    }
}
