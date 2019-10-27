﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses
{
    public class Ctg : IFunction
    {
        public IFunction Argument { get; set; }
        public double ComputeValue()
        {
            return 1.0 / Math.Tan(Argument.ComputeValue());
        }
    }
}
