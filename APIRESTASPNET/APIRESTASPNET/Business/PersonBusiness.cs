using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIRESTASPNET.Models;
using APIRESTASPNET.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIRESTASPNET.Business
{
    public class PersonBusiness
    {

        private readonly PersonService _service;

        public PersonBusiness(PersonService service)
        {
            _service = service;
        }
        public async Task<bool> Create(Person person)
        {
            return await _service.Create(person);
        }

        public async Task<bool> Delete(long id)
        {
            return await _service.Delete(id);
        }

        public async Task<List<Person>> FindById(long id)
        {
            return await _service.FindById(id);
        }

        public async Task<List<Person>> FindAll()
        {
            return await _service.FindAll();
        }

        public async Task<bool> Update(Person person)
        {
            return await _service.Update(person);
        }
    }
}