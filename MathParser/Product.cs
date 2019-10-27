using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses
{
    public class Product : IFunction
    {
        public Product()
        {
            Factors = new List<IFunction>();
        }
        public List<IFunction> Factors { get; set; }
        public double ComputeValue()
        {
            var result = 1.0;
            Factors.ForEach(s => result *= s.ComputeValue());
            return result;
        }
    }
}
