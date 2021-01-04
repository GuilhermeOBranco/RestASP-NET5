using System;

namespace APIRESTASPNET.Models
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public Person(long id, string firstname, string lastname, string address, string gender)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            Address = address;
            Gender = gender;
        }
    }
}