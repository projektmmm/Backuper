using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace backuperApi.Controllers
{
    [Authorize]
    public class BackupReport_Controller : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/admin/backup-reports/{username}")]
        public List<BackupReport> Get(string username)
        {
            var query = from br in this.database.BackupReport
                        join ba in this.database.Backups
                        on br.BackupId equals ba.Id
                        join da in this.database.Daemons
                        on ba.DaemonId equals da.Id
                        join us in this.database.Users
                        on da.UserId equals us.Id
                        where us.Username == username
                        select br;

            return query.ToList();
        }

        [HttpGet]
        [Route("api/admin/backup-reports/{username}-{daemonId}")]
        public List<BackupReport> Get(string username, int daemonId)
        {
            var query = from br in this.database.BackupReport
                        join ba in this.database.Backups
                        on br.BackupId equals ba.Id
                        join da in this.database.Daemons
                        on ba.DaemonId equals da.Id
                        join us in this.database.Users
                        on da.UserId equals us.Id
                        where us.Username == username && ba.DaemonId == daemonId
                        select br;

            return query.ToList<BackupReport>();
        }
    }
}