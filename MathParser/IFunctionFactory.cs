using MathParserClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserClasses
{
    public interface IFunctionFactory
    {
        bool Check(string expression);

        IFunction Create(string expression, ICollection<Variable> variables = null);
    }
}
