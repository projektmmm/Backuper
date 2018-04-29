using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using NCrontab;

namespace backuperApi.Controllers
{
    public class PlannedBackups_Controller : ApiController
    {
        public Database database = new Database();
        public CrontabSchedule Schedule;

        [HttpGet]
        [Route("api/admin/planned-backups")]
        public List<Backups> Get()
        {
            return this.database.Backups.ToList();
        }

        [HttpGet]
        [Route("api/admin/planned-backups/{username}-{daemonId}")]
        public List<Backups> Get(string username, int daemonId)
        {
            //inner join v entity frameworku
            var query = from b in this.database.Backups
                        join u in this.database.Users
                        on b.UserId equals u.Id
                        where u.Username == username && b.DaemonId == daemonId
                        select b;

            this.UpdateCron(query.ToList<Backups>());
            return query.ToList<Backups>();
        }

        [HttpPost]
        [Route("api/admin/daemon-settings")]
        public bool Post(Backups toInsert)
        {
            //ještě než se to uloží do databáze tak to vypočítá z cronu nextrun
            Schedule = CrontabSchedule.Parse(toInsert.Cron);
            toInsert.NextRun = Schedule.GetNextOccurrence(DateTime.Now);
            

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
            dbRecord.NextRun = toUpdate.NextRun;
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

        private async void UpdateCron(List<Backups> toCheck)
        {
            foreach (Backups item in toCheck)
            {
                Schedule = CrontabSchedule.Parse(item.Cron);

                Backups dbRecord = this.FindById(item.Id);
                dbRecord.NextRun = Schedule.GetNextOccurrence(DateTime.Now);
                this.database.SaveChanges();
            }
        }
    }
}