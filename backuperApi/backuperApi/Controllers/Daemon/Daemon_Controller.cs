using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace backuperApi.Controllers.Daemon
{
    public class Daemon_Controller : ApiController
    {
        private Database database = new Database();

        [HttpPost]
        [Route("api/daemon")]
        public void Post([FromBody] BackupReport report)
        {
            this.database.BackupReport.Add(report);
            this.database.SaveChanges();
        }

        [HttpGet]
        [Route("api/daemon")]
        public Backups Get()
        {
            return this.database.Backups.Where(b => b.RunAt > DateTime.Now).First<Backups>();
        }

    }
}