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
        [Route("api/admin/daemons/{i}")]
        public List<Daemons> Get(int i)
        {
            return this.database.Daemons.Where(d => d.UserId == i).ToList<Daemons>();
        }
    }
}