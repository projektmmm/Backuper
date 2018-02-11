using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonController : ApiController
    {

        //Api/Person
        public IEnumerable<Person> Get()
        {
            return this.FindAll();
        }

        public Person Get(int id)
        {
            return this.FindAll().Where(x => x.Id == id).First();
        }

        public void Post([FromBody]Person person)
        {
            int a = 0;
        }


        private List<Person> FindAll()
        {
            return new List<Person>()
            {
                new Person() { Id = 1, Name = "Pepa", Surname = "Novak", Age = 52 },
                new Person() { Id = 2, Name = "Karel", Surname = "Král", Age = 52 },
                new Person() { Id = 3, Name = "Viktor", Surname = "Lusk", Age = 52 },
            };
        }
    }
}