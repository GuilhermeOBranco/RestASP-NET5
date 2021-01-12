using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIRESTASPNET.Business;
using APIRESTASPNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIRESTASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/Person/v{version:ApiVersion}")]
    public class PersonController : Controller
    {
        private readonly PersonBusiness _PersonBusiness;

        public PersonController(PersonBusiness service)
        {
            _PersonBusiness = service;
        }

        [HttpGet]
        [Route("/api/Person")]
        public async Task<IActionResult> Get()
        {
            var pessoas = (await _PersonBusiness.FindAll());
            return Ok(pessoas);
        }

        [HttpGet]
        [Route("/api/Person/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var person = await _PersonBusiness.FindById(id);
            if (person.Count == 0) return NotFound("Pessoa não encontrada");
            return Ok(await _PersonBusiness.FindById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            string retorno = "Erro ao criar o usuário";
            if (person == null) return BadRequest();
            if (await _PersonBusiness.Create(person))
            {
                retorno = "usuário criado";
            }
            return Ok(retorno);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Person person)
        {
            string retorno = "Pessoa não atualizada";
            if (await _PersonBusiness.Update(person)) retorno = " Pessoa atualizada";
            if (person == null) return BadRequest();
            return Ok(retorno);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0) return NotFound("Id não existe");
            if(await _PersonBusiness.Delete(id)) return Ok("Usuário excluido!");
            return NoContent();
        }
    }
}