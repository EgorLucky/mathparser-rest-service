using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgorLucky.MathParser;
using Microsoft.AspNetCore.Mvc;
using MathParserService.DL;
using MathParserService.DL.Models;

namespace RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {
        private readonly IMathParserService _mathParserService;

        public MathController(IMathParserService mathParserService)
        {
            _mathParserService = mathParserService;
        }

        /// <summary>
        /// Вычисляет значение функции с N аргументами в заданной точке
        /// </summary>
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
        /// Получает последние limit вычесленные функции
        /// </summary>
        /// <param name="limit">Максимальное число результатов</param>
        /// <returns></returns>
        [HttpGet("getLast")]
        public async Task<ActionResult> GetLast([FromQuery]int limit = 20)
        {
            var result = await _mathParserService.GetLastAsync(limit);

            return Ok(result);
        }
    }
}
