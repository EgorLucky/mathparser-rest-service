﻿using MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses.Functions
{
    public class Sin : IFunction
    {
        public string Name => nameof(Sin);
        public IFunction Argument { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            var argumentValue = Argument.ComputeValue(variables);
            var result = Math.Sin(argumentValue);
            return result;
        }
    }
}
