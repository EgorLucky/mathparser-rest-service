﻿using EgorLucky.MathParser;

namespace MathParserService.DL.ApiModels
{
    public record ComputeExpressionResponseModel(bool IsSuccessfulComputed, string ErrorMessage = "")
    {
        public double Result { get; internal set; }
        public IExpression Expression { get; internal set; }
    }
}