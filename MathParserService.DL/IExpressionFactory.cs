using EgorLucky.MathParser;
using System.Collections.Generic;

namespace MathParserService.DL
{
    public interface IExpressionFactory<ExpressionType>
    {
        ExpressionType Create(MathTryParseResult parseResult, List<Variable> variables, List<Parameter> parameters, double computeResult);
    }
}