using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserService.DL.ApiModels
{
    public class ComputeFunctionValueResult
    {
        public double Value { get; internal init; }
        public List<Parameter> Parameters { get; internal init; }
    }
}
