using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UsingTask.Shared;

namespace UsingTask.Service.Controllers
{
    public class PeopleController : ApiController
    {
        List<Person> people = People.GetPeople();

        // GET api/<controller>
        public IEnumerable<Person> Get()
        {
            return people;
        }

        // GET api/<controller>/5
        public Person Get(int id)
        {
            return people.SingleOrDefault(p => p.Id == id);
        }

        // POST api/<controller>
        public void Post([FromBody]Person value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Person value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}