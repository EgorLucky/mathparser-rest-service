using EgorLucky.MathParser;
using System.Collections.Generic;

namespace MathParserService.DL.ApiModels
{
    public record ComputeExpressionRequestModel
    {
        public string Expression { get; set; }

        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}