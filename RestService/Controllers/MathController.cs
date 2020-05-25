using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathParser;
using Microsoft.AspNetCore.Mvc;
using RestService.Models;

namespace RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {

        private readonly MathParserClasses.MathParser _mathParser;

        public MathController(MathParserClasses.MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        /// <summary>
        /// Вычисляет значение функции с N аргументами в заданной точке
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("computeExpression")]
        public ActionResult ComputeExpression([FromBody] ComputeExpressionRequestModel request)
        {
            try 
            { 
                var variables = request.Parameters.Select(p => p.GetVariable()).ToList();
                var parsedFunction = _mathParser.Parse(request.Expression, variables);
                var result = parsedFunction.ComputeValue(request.Parameters);

                return Ok(new { 
                    result,
                    parsedFunction
                });
            }
            catch(MathParserException e)
            {
                //
                return StatusCode(500, new { e.Message });
            }
        }
        /// <summary>
        /// Вычисляет значения функции с N аргументами в заданных точках
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("computeFunctionValues")]
        public ActionResult ComputeFunctionValues([FromBody] ComputeFunctionRequestModel request)
        {
            //validate
            var functionDimensionCount = request.ParametersTable.FirstOrDefault()?.Count;
            if (functionDimensionCount == null)
                return BadRequest();

            if (request.ParametersTable.Any(p => p.Count != functionDimensionCount))
                return BadRequest();
            //get variables
            var variables = request.ParametersTable
                                    .SelectMany(p => p)
                                    .Select(p => p.GetVariable())
                                    .Distinct(new VariableEqualityComparer())
                                    .ToList();

            if (variables.Count != functionDimensionCount)
                return BadRequest();

            try
            {
                //parse
                var parsedFunction = _mathParser.Parse(request.Expression, variables);

                //compute
                var result = request.ParametersTable
                                    .Select(parameters => new
                                    {
                                        value = parsedFunction.ComputeValue(parameters),
                                        parameters
                                    })
                                    .ToList();

                return Ok(new
                {
                    result,
                    parsedFunction
                });
            }
            catch(MathParserException e)
            {
                //
                return StatusCode(500, new { e.Message });
            }
        }




        [HttpGet("test")]
        public ActionResult Test([FromServices] PseudoConfig request)
        {
            try
            {
                return Ok(request);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        
    }
}
