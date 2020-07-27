using EgorLucky.MathParser;
using System.Collections.Generic;

namespace RestService.Models
{
    public class ComputeExpressionRequestModel
    {
        public string Expression { get; set; }

        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}