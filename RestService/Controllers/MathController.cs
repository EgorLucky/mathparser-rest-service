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
        // GET api/values
        [HttpPost("computeExpression")]
        public ActionResult ComputeExpression([FromBody] ComputeExpressionRequestModel request)
        {
            try{
                var result = MathParserClasses.MathParser.Parse(request.Expression).ComputeValue();

                return Ok(new { 
                    result
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new { message = e.Message});
            }
        }

        
    }
}
