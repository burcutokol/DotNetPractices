using Bogus;
using Microsoft.AspNetCore.Identity;
using NewApi.Models;
using System.Collections.Generic;

namespace NewApi.Operation
{
    public static class Persons
    {
        private static List<Models.Person> PersonList;

        public static List<Models.Person> GetPersons()
        {
            PersonList = new Faker<Models.Person>().RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName()).Generate(20);   
            return PersonList;
            
        }
        public static Models.Person GetPersonById(int Id)
        {
            return PersonList.Find(p => p.Id == Id); 
        }

    }
}
