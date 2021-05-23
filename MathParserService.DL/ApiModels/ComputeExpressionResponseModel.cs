using EgorLucky.MathParser;

namespace MathParserService.DL.ApiModels
{
    public record ComputeExpressionResponseModel(bool IsSuccessfulComputed, string ErrorMessage = "")
    {
        public double Result { get; internal init; }
        public IExpression Expression { get; internal init; }
    }
}