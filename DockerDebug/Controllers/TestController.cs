using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DockerDebug.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
        [HttpGet("get")]
        public string Get()
        {
            return $"Hello from TestController at {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}";
        }
    }
}
