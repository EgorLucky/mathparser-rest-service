using EgorLucky.MathParser;
using System.Collections.Generic;

namespace MathParserService.DL.ApiModels
{ 
    public class ComputeFunctionRequestModel
    {
        public string Expression { get; set; }

        public List<List<Parameter>> ParametersTable { get; set; }
    }
}