using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.ApiModels
{
    public record Compute2DIntervalPlotResponseModel(bool IsSuccessfulComputed)
    {
        public string ErrorMessage { get; set; }
        public List<Point2D> Result { get;  set; }
        public IExpression Expression { get;  set; }
    }
}
