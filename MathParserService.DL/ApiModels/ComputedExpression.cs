using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.ApiModels
{
    public class ComputedExpression
    {
        public string FunctionNotation { get; set; }

        public List<ParametersAndValue> ParametersAndValues { get; set; }

        public bool LoadMore { get; set; }
    }

    public class ParametersAndValue
    {
        public string Parameters { get; set; }

        public double Value { get; set; }
    }
}
