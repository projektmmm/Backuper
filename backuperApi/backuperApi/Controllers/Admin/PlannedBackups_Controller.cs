using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using NCrontab;

namespace backuperApi.Controllers
{
    [Authorize]
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
                        join d in this.database.Daemons
                        on b.DaemonId equals d.Id
                        join u in this.database.Users
                        on d.UserId equals u.Id
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
            }

            return query.ToList();
        }

        [HttpGet]
        [Route("api/admin/planned-backups/{username}-{daemonId}")]
        public List<Backups> Get(string username, int daemonId)
        {
            //inner join v entity frameworku
            var query = from b in this.database.Backups
                        join d in this.database.Daemons
                        on b.DaemonId equals d.Id
                        join u in this.database.Users
                        on d.UserId equals u.Id
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
            }

            return query.ToList<Backups>();
        }

        [HttpPost]
        [Route("api/admin/daemon-settings")]
        public bool Post(NewBackup toInsert)
        {
            Backups backups = new Backups()
            {
                BackupType = toInsert.BackupType,
                Cron = toInsert.Cron,
                DaemonId = toInsert.DaemonId,
                DestinationPath = toInsert.DestinationPath,
                SourcePath = toInsert.SourcePath,
                Batches = (toInsert.BatchAfterCode == null && toInsert.BatchAfterPath == null && toInsert.BatchBeforeCode == null && toInsert.BatchBeforePath == null) ? false : true,
                Ftp = (toInsert.FTPServerAdress == null) ? false : true,
                Ssh = (toInsert.SSHServerAdress == null) ? false : true,
                Override = toInsert.Override,
                Rar = toInsert.Rar
            };

            if (backups.Cron.Substring(0, 1) == "#")
            {
                backups.NextRun = Convert.ToDateTime(backups.Cron.Substring(1, backups.Cron.Length));
                backups.Cron = "* * * * *";
            }
            else
            {
                //ještě než se to uloží do databáze tak to vypočítá z cronu nextrun
                Schedule = CrontabSchedule.Parse(backups.Cron);
                backups.NextRun = Schedule.GetNextOccurrence(DateTime.Now);
            }

            backups.SourcePath = "[\"" + backups.SourcePath.Replace(",", "\",\"").Replace("\\", "\\\\") + "\"]";
            backups.DestinationPath = "[\"" + backups.DestinationPath.Replace(",", "\",\"").Replace("\\", "\\\\") + "\"]";

            this.database.Backups.Add(backups);
            this.database.SaveChanges();

            Backups dbRecord = this.database.Backups.Find(backups);
            int id = dbRecord.Id;

            if (dbRecord.Ftp)
            {
                this.database.FtpSettings.Add(new FTPSettings()
                {
                    BackupId = id,
                    Password = toInsert.FTPPassword,
                    Port = toInsert.FTPPort,
                    ServerAdress = toInsert.FTPServerAdress,
                    Username = toInsert.FTPUsername
                });
                this.database.SaveChanges();
            }

            if (dbRecord.Ssh)
            {
                this.database.SshSettings.Add(new SSHSettings()
                {
                    BackupId = id,
                    HostOrIp = toInsert.SSHServerAdress,
                    Password = toInsert.SSHPassword,
                    Port = toInsert.SSHPort,
                    Username = toInsert.SSHUsername
                });
                this.database.SaveChanges();
            }

            if (dbRecord.Batches)
            {
                if (toInsert.BatchAfterCode != null)
                {
                    this.database.Batches.Add(new Batches()
                    {
                        BackupId = id,
                        CommandText = toInsert.BatchAfterCode,
                        Time = "AFTER",
                        Type = "COMMAND"
                    });
                    this.database.SaveChanges();
                }
                if (toInsert.BatchBeforeCode != null)
                {
                    this.database.Batches.Add(new Batches()
                    {
                        BackupId = id,
                        CommandText = toInsert.BatchBeforeCode,
                        Time = "BEFORE",
                        Type = "COMMAND"
                    });
                    this.database.SaveChanges();
                }
                if (toInsert.BatchAfterPath != null)
                {
                    this.database.Batches.Add(new Batches()
                    {
                        BackupId = id,
                        CommandText = toInsert.BatchAfterPath,
                        Time = "AFTER",
                        Type = "SAVED"
                    });
                    this.database.SaveChanges();
                }
                if (toInsert.BatchBeforePath != null)
                {
                    this.database.Batches.Add(new Batches()
                    {
                        BackupId = id,
                        CommandText = toInsert.BatchBeforePath,
                        Time = "AFTER",
                        Type = "SAVED"
                    });
                    this.database.SaveChanges();
                }
            }

            return true;
        }

        [HttpPatch]
        [Route("api/admin/planned-backups/{id}")]
        public bool Patch(Backups toUpdate)
        {  
            Backups dbRecord = this.FindById(toUpdate.Id);

            Schedule = CrontabSchedule.Parse(toUpdate.Cron);
            dbRecord.NextRun = Schedule.GetNextOccurrence(DateTime.Now);

            dbRecord.DaemonId = toUpdate.DaemonId;
            dbRecord.BackupType = toUpdate.BackupType;
            dbRecord.Cron = toUpdate.Cron;
            dbRecord.DestinationPath = toUpdate.DestinationPath;
            ////dbRecord.NextRun = toUpdate.NextRun;
            dbRecord.SourcePath = toUpdate.SourcePath;

            dbRecord.SourcePath = "[\"" + toUpdate.SourcePath.Replace(",", "\",\"").Replace("\\", "\\\\") + "\"]";
            dbRecord.DestinationPath = "[\"" + toUpdate.DestinationPath.Replace(",", "\",\"").Replace("\\", "\\\\") + "\"]";

            this.database.SaveChanges();
                
            return true;
        }

        /// Odstraneni naplanovaneho backupu
        [HttpDelete]
        [Route("api/admin/planned-backups/{id}")]
        public bool Delete(int id)
        {
            try
            {
                foreach (BackupReport item in this.database.BackupReport.Where(b => b.BackupId == id).ToList())
                {
                    foreach (BackupErrors error in this.database.BackupErrors.Where(e => e.BackupReportId == item.Id).ToList())
                    {
                        this.database.BackupErrors.Remove(error);
                        this.database.SaveChanges();
                    }
                    this.database.BackupReport.Remove(item);
                    this.database.SaveChanges();
                }

                foreach (FTPSettings item in this.database.FtpSettings.Where(f => f.BackupId == id).ToList())
                {
                    this.database.FtpSettings.Remove(item);
                    this.database.SaveChanges();
                }

                foreach (SSHSettings item in this.database.SshSettings.Where(s => s.BackupId == id).ToList())
                {
                    this.database.SshSettings.Remove(item);
                    this.database.SaveChanges();
                }

                Backups del = this.FindById(id);
                this.database.Backups.Remove(del);

                this.database.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private Backups FindById(int id)
        {
            return this.database.Backups.Find(id);
        }
    }
}