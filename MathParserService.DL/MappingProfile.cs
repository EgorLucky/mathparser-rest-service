using AutoMapper;
using EgorLucky.MathParser;
using MathParserService.DAL;
using Newtonsoft.Json;

namespace MathParserService.DL
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

        }
    }
}
