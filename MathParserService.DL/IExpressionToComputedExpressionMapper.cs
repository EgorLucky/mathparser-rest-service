using MathParserService.DL.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    public interface IExpressionToComputedExpressionMapper<ExpressionType>
    {
        ComputedExpression Map(ExpressionType expression);
    }
}
