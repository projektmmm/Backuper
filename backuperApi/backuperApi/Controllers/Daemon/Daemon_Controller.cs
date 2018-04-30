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
        private CronController cronController = new CronController();

        [HttpGet]
        [Route("api/daemon/{daemonId}")]
        public List<Backups> Get(string daemonId)
        {
            int dId = Convert.ToInt32(daemonId);
            this.cronController.UpdateCron(this.database.Backups.Where(b => b.DaemonId == dId).ToList<Backups>());
            return this.database.Backups.Where(b => b.DaemonId == dId).ToList<Backups>();
        }

        [HttpPost]
        [Route("api/daemon")]
        public void Post([FromBody] BackupReport report)
        {
            this.database.BackupReport.Add(report);
            this.database.SaveChanges();
        }


        [HttpPut]
        [Route("api/admin/daemon/{username}")]
        public bool Put(Daemons daemons, string username)
        {
            try
            {
                Daemons dbRecord = this.FindById(username, daemons.Id);

                dbRecord.Name = daemons.Name;
                dbRecord.Description = daemons.Description;
                this.database.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private Daemons FindById(string username, int daemonId)
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