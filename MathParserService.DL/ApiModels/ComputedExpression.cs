using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.ApiModels
{
    public record ComputedExpression
    {
        public string FunctionNotation { get; init; }

        public List<ParametersAndValue> ParametersAndValues { get; init; }

        public bool LoadMore { get; init; }
    }

    public record ParametersAndValue
    {
        public string Parameters { get; init; }

        public double Value { get; init; }
    }
}
