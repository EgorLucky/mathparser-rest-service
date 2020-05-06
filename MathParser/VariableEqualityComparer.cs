using MathParserClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser
{
    public class VariableEqualityComparer : IEqualityComparer<Variable>
    {
        public bool Equals(Variable x, Variable y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Variable obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
