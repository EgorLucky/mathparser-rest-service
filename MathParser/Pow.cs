using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses
{
    public class Pow : IFunction
    {
        public IFunction Log { get; set; }
        public IFunction Base { get; set; }
        public double ComputeValue()
        {
            return Math.Pow(Base.ComputeValue(), Log.ComputeValue());
        }
    }
}
