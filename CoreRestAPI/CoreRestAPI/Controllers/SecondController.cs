using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRestAPI.Controllers
{
    [Produces("application/json")]    
    [ApiVersion("2.0")]
    [ApiController]    
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SecondController : ControllerBase
    {
        /// <summary>
        /// Return hardcoded values for version
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetVersionList()
        {
            var data = new List<string>();
            data.Add("1.0");
            data.Add("2.0");
            data.Add("3.0");
            return Ok(data);
        }
    }
}
