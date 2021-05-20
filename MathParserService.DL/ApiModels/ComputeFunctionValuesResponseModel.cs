using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserService.DL.ApiModels
{
    public record ComputeFunctionValuesResponseModel(bool IsSuccessfulComputed, string ErrorMessage = "")
    {
        public IExpression Expression { get; internal set; }
        public List<ComputeFunctionValueResult> Result { get; internal set; }
    }
}
