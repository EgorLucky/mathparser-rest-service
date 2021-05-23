using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.ApiModels
{
    public record Point2D
    {
        public double X { get; init; }

        public double Y { get; init; }
    }
}
