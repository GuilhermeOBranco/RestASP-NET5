using System;
using Microsoft.AspNetCore.Mvc;

namespace APIRESTASPNET.Controllers
{
    public class PersonController: Controller
    {
        [HttpGet("api/Person")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}