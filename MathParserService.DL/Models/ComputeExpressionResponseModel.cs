using EgorLucky.MathParser;

namespace MathParserService.DL.Models
{
    public class ComputeExpressionResponseModel
    {
        public bool IsSuccessfulComputed { get;  set; }
        public string ErrorMessage { get;  set; }
        public double Result { get; internal set; }
        public IExpression Expression { get; internal set; }
    }
}