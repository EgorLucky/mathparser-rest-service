using EgorLucky.MathParser;
using System.Collections.Generic;

namespace MathParserService.DL.ApiModels
{
    public record ComputeExpressionRequestModel
    {
        public string Expression { get; init; }

        public List<Parameter> Parameters { get; init; } = new List<Parameter>();
    }
}