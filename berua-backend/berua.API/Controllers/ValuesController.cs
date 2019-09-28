using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berau_backend.Model;
using Microsoft.AspNetCore.Mvc;

namespace berua.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult GetTest()
        {
            var dict = new Dictionary<SocialNetworkType, string>()
            { 
                {SocialNetworkType.vk, "xfkgjbns;gbk"},
                {SocialNetworkType.facebook, "dtyjd"},
                {SocialNetworkType.instagram, "dydyhnsthsrth;gbk"},
            };
            return Ok(dict);
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
