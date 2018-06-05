using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace backuperApi.Controllers.Daemon
{
    public class FtpController : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/daemon/ftp/{daemonId}")]
        public List<FTPSettings> Get(int daemonId)
        {
            var query = from f in this.database.FtpSettings
                        join b in this.database.Backups
                        on f.BackupId equals b.Id
                        where b.DaemonId == daemonId
                        select f;

            return query.ToList<FTPSettings>();
        }
    }
}