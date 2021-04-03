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
                                options => options.MapFrom(m =>
                                                $"F({string.Join(",", m.Parameters.Select(p => p.Name))}) = {m.ExpressionString}"))
                .ForMember(e => e.ParametersAndValues,
                                options => options.MapFrom(m => 
                                                m.Points.Select(p => 
                                                    Map(p)
                                                )
                                                .ToList()));
        }


        ParametersAndValue Map(Point p)
        {
            var result = new ParametersAndValue
            {
                Parameters = p.Coordinates == null ? 
                                    "F()" : 
                                    $"F({string.Join(",", p.Coordinates?.Select(c => c.Value))})",
                Value = p.Result
            };

            return result;
        }
    }
}
