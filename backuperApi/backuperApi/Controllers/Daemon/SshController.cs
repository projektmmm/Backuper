using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace backuperApi.Controllers.Daemon
{
    public class SshController : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/daemon/ssh/{daemonId}")]
        public List<SSHSettings> Get(int daemonId)
        {
            var query = from s in this.database.SshSettings
                        join b in this.database.Backups
                        on s.BackupId equals b.Id
                        where b.DaemonId == daemonId
                        select s;

            return query.ToList<SSHSettings>();
        }
    }
}