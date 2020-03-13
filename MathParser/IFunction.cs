using MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses
{
    public interface IFunction
    {
        double ComputeValue(ICollection<Parameter> variables);
    }
}
