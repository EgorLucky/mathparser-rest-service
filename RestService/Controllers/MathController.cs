using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        // GET api/values
        [HttpPost("computeExpression")]
        public ActionResult ComputeExpression([FromBody] ComputeExpressionRequestModel request)
        {
            try
            {
                var variables = request.Parameters.Select(p => p.Variable).ToList();
                var parsedFunction = _mathParser.Parse(request.Expression, variables);
                var result = parsedFunction.ComputeValue(request.Parameters);

                return Ok(new { 
                    result,
                    parsedFunction
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new { message = e.Message});
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
