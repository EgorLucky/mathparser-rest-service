using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserService.DL.Models
{
    public class ComputeFunctionValueResult
    {
        public double Value { get; internal set; }
        public List<Parameter> Parameters { get; internal set; }
    }
}
