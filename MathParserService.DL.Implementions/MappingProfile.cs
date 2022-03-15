using AutoMapper;
using EgorLucky.MathParser;
using MathParserService.DAL;
using MathParserService.DL.ApiModels;
using Newtonsoft.Json;
using System.Linq;

namespace MathParserService.DL.Implementions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<MathTryParseResult, Expression>()
                .ForMember(e => e.ExpressionString, options => options.MapFrom(m => m.InputString))
                .ForMember(e => e.ExpressionJson, options => options.MapFrom(m => JsonConvert.SerializeObject(m.Expression)));

            CreateMap<Variable, DAL.Parameter>();


            CreateMap<Expression, ComputedExpression>()
                .ForMember(e => e.FunctionNotation,
                                options => options.MapFrom(m => GetFunuctionStringNotation(m)))
                .ForMember(e => e.ParametersAndValues,
                                options => options.MapFrom(m => m.Points.Select(p => Map(p))
                                                                        .ToList()));
        }


        ParametersAndValue Map(Point p)
        {
            var parameterValueListString = "";

            if (p.Coordinates != null)
            {
                var parameterValueList = p.Coordinates?.Select(c => c.Value);
                parameterValueListString = string.Join(",", parameterValueList);
            }

            var result = new ParametersAndValue
            {
                Id = p.Id,
                Parameters = $"F({parameterValueListString})",
                Value = p.Result
            };

            return result;
        }

        string GetFunuctionStringNotation(Expression exp)
        {
            var variableList = exp.Parameters.Select(p => p.Name);
            var variableListString = string.Join(",", variableList);

            return $"F({variableListString}) = {exp.ExpressionString}";
        }

    }
}
