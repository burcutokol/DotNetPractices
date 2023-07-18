using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NewApi.Models;
using NewApi.Operation;

namespace NewApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PersonsController : ControllerBase
    {

        [HttpGet]
        public List<Person> Person()
        {
            return Persons.GetPersons();
        }
        [HttpGet("{id}")]
        public Person GetPersonById(int Id) {
            return Persons.GetPersonById(Id);   
        }
        [HttpGet] 
        public Person GetPersonByIdQuery([FromQuery] int Id) {
            return Persons.GetPersonById(Id);
        }
    }
}
