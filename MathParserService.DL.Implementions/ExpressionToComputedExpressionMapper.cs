using AutoMapper;
using MathParserService.DAL;
using MathParserService.DL.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserService.DL.Implementions
{
    public class ExpressionToComputedExpressionMapper : IExpressionToComputedExpressionMapper<Expression>
    {
        private readonly IMapper _mapper;

        public ExpressionToComputedExpressionMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ComputedExpression Map(Expression expression)
        {
            return _mapper.Map<ComputedExpression>(expression);
        }
    }
}
