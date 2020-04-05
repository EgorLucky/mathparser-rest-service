using MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses.Functions
{
    public class Tg : IFunction
    {
        public IFunction Argument { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Math.Tan(Argument.ComputeValue(variables));
        }
    }
}
