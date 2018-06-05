using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace backuperApi.Controllers.Daemon
{
    public class BatchesController : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/daemon/batches/{daemonId}")]
        public List<Batches> Get(int daemonId)
        {
            var query = from bc in this.database.Batches
                        join ba in this.database.Backups
                        on bc.BackupId equals ba.Id
                        where ba.DaemonId == daemonId
                        select bc;

            return query.ToList<Batches>();
        }
    }
}