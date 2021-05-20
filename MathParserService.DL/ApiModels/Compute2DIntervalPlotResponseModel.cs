using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.ApiModels
{
    public record Compute2DIntervalPlotResponseModel(bool IsSuccessfulComputed, string ErrorMessage = "")
    {
        public List<Point2D> Result { get;  init; }
        public IExpression Expression { get;  init; }
    }
}
