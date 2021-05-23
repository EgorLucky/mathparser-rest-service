using EgorLucky.MathParser;
using System.Collections.Generic;

namespace MathParserService.DL.ApiModels
{ 
    public record ComputeFunctionRequestModel
    {
        public string Expression { get; init; }

        public List<List<Parameter>> ParametersTable { get; init; }
    }
}