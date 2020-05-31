﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Constants
{
    public class E: IConst
    {
        public string Name => "e";

        public double Value => Math.E;
    }
}
