using MathParserService.DAL;
using MathParserService.DL;
using MathParserService.DL.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MathController : ControllerBase
    {
        private readonly IMathParserService<Expression> _mathParserService;

        public MathController(IMathParserService<Expression> mathParserService)
        {
            _mathParserService = mathParserService;
        }

        /// <summary>
        /// Вычисляет значение функции с N аргументами в заданной точке
        /// </summary>
        /// <remarks>
        /// Образец запроса:
        ///
        ///     {
        ///         "expression":"pi^x",
        ///         "parameters":[
        ///                 {
        ///                     "variableName":"x",
        ///                     "value":0
        ///                 }
        ///             ]
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("computeExpression")]
        public async Task<ActionResult> ComputeExpression([FromBody] ComputeExpressionRequestModel request)
        {
            var result = await _mathParserService.ComputeExpression(request);

            if (!result.IsSuccessfulComputed)
                return StatusCode(500, new
                {
                    Message = result.ErrorMessage
                });

            return Ok(result);
        }

        /// <summary>
        /// Вычисляет значения функции с N аргументами в заданных точках
        /// </summary>
        /// <remarks>
        /// Образец запроса:
        ///
        ///     {
        ///         "expression":"x^2",
        ///         "parametersTable":[
        ///             [
        ///                 {
        ///                     "variableName":"x",
        ///                     "value":0
        ///                 }
        ///             ],
        ///             [
        ///                 {
        ///                     "variableName":"x",
        ///                     "value":1
        ///                 }
        ///             ],
        ///             [
        ///                 {
        ///                     "variableName":"x",
        ///                     "value":2
        ///                 }
        ///             ],
        ///             [
        ///                 {
        ///                     "variableName":"x",
        ///                     "value":3
        ///                 }
        ///             ],
        ///             [
        ///                 {
        ///                     "variableName":"x",
        ///                     "value":4
        ///                 }
        ///             ]
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("computeFunctionValues")]
        public async Task<ActionResult> ComputeFunctionValues([FromBody] ComputeFunctionRequestModel request)
        {
            var result = await _mathParserService.ComputeFunctionValues(request);

            if (!result.IsSuccessfulComputed)
                return StatusCode(500, new
                {
                    Message = result.ErrorMessage
                });

            return Ok(result);

        }

        /// <summary>
        /// Получает последние N вычисленных функций
        /// </summary>
        /// <param name="limit">Максимальное число результатов</param>
        /// <returns></returns>
        [HttpGet("getLast")]
        public async Task<ActionResult> GetLast([FromQuery] int limit = 20)
        {
            var result = await _mathParserService.GetLastAsync(limit);

            return Ok(result);
        }
    }
}
