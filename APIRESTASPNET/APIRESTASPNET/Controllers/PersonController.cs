using System;
using APIRESTASPNET.Models;
using APIRESTASPNET.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIRESTASPNET.Controllers
{
    public class PersonController: Controller
    {
        private readonly IPersonService _PersonService;

        public PersonController(IPersonService service)
        {
            _PersonService = service;
        }

        [HttpGet("api/Person")]
        public IActionResult Get()
        {
            return Ok(_PersonService.FindAll());
        }

        [HttpGet("api/Person{id}")]
        public IActionResult Get(long id)
        {
            var person = _PersonService.FindById(id);
            if(person == null) return NotFound();
            return Ok(_PersonService.FindAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if(person == null) return BadRequest();
            return Ok();
        }
    }
}