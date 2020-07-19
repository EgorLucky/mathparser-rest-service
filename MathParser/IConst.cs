using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser
{
    public interface IConst : IMathParserEntity
    {
        double Value { get;}
    }
}
