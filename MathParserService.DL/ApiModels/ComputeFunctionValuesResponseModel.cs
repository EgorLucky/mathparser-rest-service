using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserService.DL.ApiModels
{
    public class ComputeFunctionValuesResponseModel
    {
        public bool IsSuccessfulComputed { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public IExpression Expression { get; internal set; }
        public List<ComputeFunctionValueResult> Result { get; internal set; }
    }
}
