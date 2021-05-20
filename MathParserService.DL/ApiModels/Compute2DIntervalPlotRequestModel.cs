using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.ApiModels
{
    public record Compute2DIntervalPlotRequestModel
    {
        public string Expression { get; init; }

        public double Min { get; init; }
        
        public double Max { get; init; }

        public double Step { get; init; }
    }
}
