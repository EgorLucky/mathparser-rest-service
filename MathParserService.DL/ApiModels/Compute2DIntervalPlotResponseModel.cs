using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.ApiModels
{
    public class Compute2DIntervalPlotResponseModel
    {
        public bool IsSuccessfulComputed { get; set; }
        public string ErrorMessage { get; set; }
        public List<Point2D> Result { get; internal set; }
        public IExpression Expression { get; internal set; }
    }
}
