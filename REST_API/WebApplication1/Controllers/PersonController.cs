using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;
using MySql.Data.MySqlClient;

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
            Sql.SetCommand($"SELECT * FROM tbPerson WHERE Id = {id}");
            Sql.Open();
            MySqlDataReader sRead = Sql.sComm.ExecuteReader();
            sRead.Read();
            Person ret =  new Person()
            {
                Id = Convert.ToInt32(sRead[0]),
                Name = sRead[1].ToString(),
                Surname = sRead[2].ToString(),
                Age = Convert.ToInt32(sRead[3])
            };
            Sql.Close();
            return ret;
        }

        public void Post([FromBody]Person person)
        {
            Sql.SetCommand($"INSERT INTO tbPerson(Name, Surname, Age) VALUES('{person.Name}','{person.Surname}','{person.Age}')");
            Sql.Open();
            Sql.sComm.ExecuteNonQuery();
            Sql.Close();
        }

        public void Put(int id, [FromBody]Person person)
        {
            Sql.SetCommand($"UPDATE tbPerson SET Name={person.Name},Surname={person.Surname},Age={person.Age} WHERE Id={id}");
            Sql.Open();
            Sql.sComm.ExecuteNonQuery();
            Sql.Close();
        }

        public void Delete(int id)
        {
            Sql.SetCommand($"DELETE FROM tbPerson WHERE Id={id}");
            Sql.Open();
            Sql.sComm.ExecuteNonQuery();
            Sql.Close();
        }


        private List<Person> FindAll()
        {
            List<Person> ret = new List<Person>();
            Sql.SetCommand($"SELECT * FROM tbPerson");
            Sql.Open();
            MySqlDataReader sRead = Sql.sComm.ExecuteReader();

            while (sRead.Read())
            {
                Person person = new Person()
                {
                    Id = Convert.ToInt32(sRead[0]),
                    Name = sRead[1].ToString(),
                    Surname = sRead[2].ToString(),
                    Age = Convert.ToInt32(sRead[3])
                };
                ret.Add(person);
            }

            Sql.Close();
            return ret;
        }
    }
}