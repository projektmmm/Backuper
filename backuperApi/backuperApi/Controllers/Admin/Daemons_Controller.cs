using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace backuperApi.Controllers
{
    public class Daemons_Controller : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/admin/daemons/{username}")]
        public List<Daemons> Get(string username)
        {
            var query = from d in this.database.Daemons
                        join u in this.database.Users
                        on d.UserId equals u.Id
                        where u.Username == username
                        select d;

            return query.ToList<Daemons>();
        }

        [HttpGet]
        [Route("api/admin/daemons/{username}-{daemonId}")]
        public Daemons Get(string username, int daemonId)
        {
            var query = from d in this.database.Daemons
                        join u in this.database.Users
                        on d.UserId equals u.Id
                        where u.Username == username && d.Id == daemonId
                        select d;

            return query.First();
        }
    }
}