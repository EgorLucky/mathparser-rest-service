using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses
{
    public class Tg : IFunction
    {
        public IFunction Argument { get; set; }
        public double ComputeValue()
        {
            return Math.Tan(Argument.ComputeValue());
        }
    }
}
