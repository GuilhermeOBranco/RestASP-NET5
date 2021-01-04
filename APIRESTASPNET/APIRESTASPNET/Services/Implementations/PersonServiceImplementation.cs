using System;
using System.Collections.Generic;
using APIRESTASPNET.Models;

namespace APIRESTASPNET.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {

        }

        public List<Person> FindById(long id)
        {
            List<Person> pessoas = new List<Person>();
            pessoas.Add(new Person(
                1,
                "Guilherme",
                "Branco",
                "Santo André",
                "masculino"
            ));

            return pessoas;
        }

        public List<Person> FindAll()
        {
            List<Person> pessoas = new List<Person>();

            return pessoas;
        }

        public Person Update(Person person)
        {
            throw new NotImplementedException();
        }

        Person IPersonService.FindById(long id)
        {
            throw new NotImplementedException();
        }
    }
}