using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIRESTASPNET.Models;
using APIRESTASPNET.Services;
using APIRESTASPNET.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace APIRESTASPNET.Controllers
{
    [ApiController]
    [Route("api/Person")]
    public class PersonController: Controller
    {
        private readonly PersonServiceImplementation _PersonService;

        public PersonController(PersonServiceImplementation service)
        {
            _PersonService = service;
        }

        [HttpGet]
        [Route("/api/Person")]
        public async Task<IActionResult> Get()
        {
            var pessoas = (await _PersonService.FindAll());
            return Ok(pessoas);
        }

        [HttpGet]
        [Route("/api/Person/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var person = await _PersonService.FindById(id);
            if(person.Count == 0) return NotFound("Pessoa não encontrada");
            return Ok(await _PersonService.FindById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            string retorno = "Erro ao criar o usuário";
            if(person == null) return BadRequest();
            if(await _PersonService.Create(person))
            {
                retorno = "usuário criado";
            }
            return Ok(retorno);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Person person)
        {
            string retorno  = "Pessoa não atualizada";
            if(await _PersonService.Update(person)) retorno = " Pessoa atualizada";
            if(person == null) return BadRequest();
            return Ok(retorno);   
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            if(id == 0) return BadRequest();
            _PersonService.Delete(id);
            return NoContent();
        }
    }
}