using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace backuperApi.Controllers
{
    public class BackupReport_Controller : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/admin/backup-reports/{username}")]
        public List<BackupReport> Get(string username)
        {
            var query = from b in this.database.BackupReport
                        join u in this.database.Users
                        on b.UserId equals u.Id
                        where u.Username == username
                        select b;

            return query.ToList();
        }

        [HttpGet]
        [Route("api/admin/backup-reports/{username}-{daemonId}")]
        public List<BackupReport> Get(string username, int daemonId)
        {
            //inner join v entity frameworku boiis
            var query = from b in this.database.BackupReport
                        join u in this.database.Users
                        on b.UserId equals u.Id
                        where u.Username == username && b.DaemonId == daemonId
                        select b;

            return query.ToList<BackupReport>();
        }
    }
}