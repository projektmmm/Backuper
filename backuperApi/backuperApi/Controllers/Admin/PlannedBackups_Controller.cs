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
        public CronController cronController = new CronController();


        [HttpGet]
        [Route("api/admin/planned-backups/{username}")]
        public List<Backups> Get(string username)
        {
            var query = from b in this.database.Backups
                        join u in this.database.Users
                        on b.UserId equals u.Id
                        where u.Username == username
                        select b;

            foreach (var item in query.ToList<Backups>())
            {
                item.SourcePath = item.SourcePath.Remove(0, 2);
                item.SourcePath = item.SourcePath.Remove(item.SourcePath.Length - 2, 2);

                item.DestinationPath = item.DestinationPath.Remove(0, 2);
                item.DestinationPath = item.DestinationPath.Remove(item.DestinationPath.Length - 2, 2);

                item.SourcePath = item.SourcePath.Replace("\\\\", "\\");
                item.SourcePath = item.SourcePath.Replace("\",\"", ",");
                item.SourcePath = item.SourcePath.Replace("*", "\\");
                item.SourcePath = item.SourcePath.Replace("\"", "");

                item.DestinationPath = item.DestinationPath.Replace("\\\\", "\\");
                item.DestinationPath = item.DestinationPath.Replace("\",\"", ",");
                item.DestinationPath = item.DestinationPath.Replace("*", "\\");
                item.DestinationPath = item.DestinationPath.Replace("\"", "");

                //item.SourcePath = item.SourcePath.Replace(",", "," + System.Environment.NewLine);
                //item.DestinationPath = item.DestinationPath.Replace(",", "," + System.Environment.NewLine);

            }

            return query.ToList();
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

            this.cronController.UpdateCron(query.ToList<Backups>());

            foreach (var item in query.ToList<Backups>())
            {
                item.SourcePath = item.SourcePath.Remove(0, 2);
                item.SourcePath = item.SourcePath.Remove(item.SourcePath.Length - 2, 2);

                item.DestinationPath = item.DestinationPath.Remove(0, 2);
                item.DestinationPath = item.DestinationPath.Remove(item.DestinationPath.Length - 2, 2);

                item.SourcePath = item.SourcePath.Replace("\\\\", "\\");
                item.SourcePath = item.SourcePath.Replace("\",\"", ",");
                item.SourcePath = item.SourcePath.Replace("*", "\\");
                item.SourcePath = item.SourcePath.Replace("\"", "");

                item.DestinationPath = item.DestinationPath.Replace("\\\\", "\\");
                item.DestinationPath = item.DestinationPath.Replace("\",\"", ",");
                item.DestinationPath = item.DestinationPath.Replace("*", "\\");
                item.DestinationPath = item.DestinationPath.Replace("\"", "");

                //item.SourcePath = item.SourcePath.Replace(",", "," + System.Environment.NewLine);
                //item.DestinationPath = item.DestinationPath.Replace(",", "," + System.Environment.NewLine);
            }

            return query.ToList<Backups>();
        }

        [HttpPost]
        [Route("api/admin/daemon-settings")]
        public bool Post(Backups toInsert)
        {
            //ještě než se to uloží do databáze tak to vypočítá z cronu nextrun
            Schedule = CrontabSchedule.Parse(toInsert.Cron);
            toInsert.NextRun = Schedule.GetNextOccurrence(DateTime.Now);

            toInsert.SourcePath = "[\"" + toInsert.SourcePath.Replace(",", "\",\"").Replace("\\","\\\\") + "\"]";
            toInsert.DestinationPath = "[\"" + toInsert.DestinationPath.Replace(",", "\",\"").Replace("\\", "\\\\") + "\"]";

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

            dbRecord.SourcePath = "[\"" + toUpdate.SourcePath.Replace(",", "\",\"").Replace("\\", "\\\\") + "\"]";
            dbRecord.DestinationPath = "[\"" + toUpdate.DestinationPath.Replace(",", "\",\"").Replace("\\", "\\\\") + "\"]";

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