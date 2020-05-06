using MathParserClasses;
using System.Collections.Generic;

namespace RestService.Models 
{ 
    public class ComputeFunctionRequestModel
    {
        public string Expression { get; set; }

        public List<List<Parameter>> ParametersTable { get; set; }
    }
}