using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.ApiModels
{
    public record Compute2DIntervalPlotRequestModel
    {
        public string Expression { get; set; }

        public double Min { get; set; }
        
        public double Max { get; set; }

        public double Step { get; set; }
    }
}
