using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses
{
    public class Exp : IFunction
    {
        public IFunction Argument { get; set; }
        public double ComputeValue()
        {
            return Math.Exp(Argument.ComputeValue());
        }
    }
}
