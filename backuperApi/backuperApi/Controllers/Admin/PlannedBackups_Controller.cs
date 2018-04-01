using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace backuperApi.Controllers
{
    public class PlannedBackups_Controller : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/admin/planned-backups")]
        public List<Backups> Get()
        {
            return this.database.Backups.ToList();
        }

        [HttpPost]
        [Route("api/admin/daemon-settings")]
        public bool Post(Backups toInsert)
        {
            this.database.Backups.Add(toInsert);
            this.database.SaveChanges();
            return true;
        }

        [HttpPatch]
        [Route("api/admin/planned-backups/{id}")]
        public bool Patch(Backups toUpdate)
        {
            Backups dbRecord = this.FindById(toUpdate.Id);

            dbRecord.DaemonId = toUpdate.DaemonId;
            dbRecord.BackupType = toUpdate.BackupType;
            dbRecord.Cron = toUpdate.Cron;
            dbRecord.DestinationPath = toUpdate.DestinationPath;
            dbRecord.RunAt = toUpdate.RunAt;
            dbRecord.SourcePath = toUpdate.SourcePath;

            this.database.SaveChanges();
                
            return true;
        }

        [HttpDelete]
        [Route("api/admin/planned-backups/{id}")]
        public bool Delete(int id)
        {
            Backups del = this.FindById(id);
            this.database.Backups.Remove(del);

            this.database.SaveChanges();
            return true;
        }

        private Backups FindById(int id)
        {
            return this.database.Backups.Find(id);
        }
    }
}