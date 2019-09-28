using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace berua.API.Controllers
{
    [Route("api/ping")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        [Route("get-version")]
        public IActionResult GetVersion()
        {
            return Ok("8");
        }
    }
}