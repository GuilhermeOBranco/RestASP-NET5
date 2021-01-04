using System;
using System.Collections.Generic;
using APIRESTASPNET.Models;

namespace APIRESTASPNET.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person Update(Person person);
        void Delete(long id);
        Person FindById(long id);
        List<Person> FindAll();
    }
}
