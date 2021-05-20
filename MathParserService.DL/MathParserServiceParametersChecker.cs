using EgorLucky.MathParser;
using MathParserService.DL.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL
{
    internal static class MathParserServiceParametersChecker
    {
        internal static string CheckForComputeExpression(ComputeExpressionRequestModel request, int maxParameterCount) 
        {
            if (request.Parameters.Count > maxParameterCount)
                return "Number of parameters is bigger than 5";

            return "";
        }

        internal static string CheckForComputeFunctionValues(ComputeFunctionRequestModel request, int? functionDimensionCount, List<Variable> variables)
        {
            if (functionDimensionCount == null)
                return "Couldn't count function's dimensions";

            if (request.ParametersTable.Any(p => p.Count != functionDimensionCount))
                return "Not equal parameters number for each point";


            if (variables.Count != functionDimensionCount)
                return "Not equal parameters number for each point";

            return "";
        }

        internal static string CheckForCompute2DIntervalPlot(Compute2DIntervalPlotRequestModel request)
        {
            if (request.Max <= request.Min)
                return "Max is not bigger than Min";

            if (Math.Abs(request.Max - request.Min) < request.Step)
                return "Step is bigger than interval between Max and Min";

            if (request.Step <= 0)
                return "Step is not bigger than zero";

            return "";
        }
    }
}
