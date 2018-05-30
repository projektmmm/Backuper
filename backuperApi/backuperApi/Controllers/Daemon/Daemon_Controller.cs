﻿using System;
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
        [Route("api/daemon/{daemonId}")]
        public void Post(int daemonId, [FromBody] string data)
        {

            List<BackupErrors> errors = new List<BackupErrors>();
            string tosub = data.Substring(0, this.IdentifyBorder(data));
            BackupReport report = JsonConvert.DeserializeObject<BackupReport>(tosub);

            try
            {
                errors = JsonConvert.DeserializeObject<List<BackupErrors>>(data.Substring(this.IdentifyBorder(data) + 10));
            }
            catch
            {
                // NO ERRORS
            }

            this.database.BackupReport.Add(report);
            this.database.SaveChanges();

            BackupReport dbRecord = this.database.BackupReport.Where(r => r.BackupId == report.BackupId && r.Date == report.Date && r.Size == report.Size && r.Type == report.Type && r.UserId == report.UserId).FirstOrDefault();
            int reportId = dbRecord.Id;

            foreach (BackupErrors item in errors)
            {
                item.BackupReportId = reportId;
                this.database.BackupErrors.Add(item);
            }
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

        /// FTP a SSH
        
        [HttpGet]
        [Route("api/daemon/ftp/{daemonId}-{userId}")]
        public List<FtpSettings> Get(int daemonId, int userId)
        {
            var query = from f in this.database.FtpSettings
                        join b in this.database.Backups
                        on f.BackupId equals b.Id
                        where b.DaemonId == daemonId && b.UserId == userId
                        select f;

            
            

            return query.ToList<FtpSettings>();
        }

        //[HttpGet]
        //[Route("api/daemon/ssh/{daemonId}-{userId}")]
        //public List<SshSettings> Get(int daemonId, int userId)
        //{
        //    var query = from s in this.database.SshSettings
        //                join b in this.database.Backups
        //                on s.BackupId equals b.Id
        //                where b.DaemonId == daemonId && b.UserId == userId
        //                select s;

        //    return query.ToList<SshSettings>();
        //}

        private Daemons FindById(string username, int daemonId)
        {
            var query = from d in this.database.Daemons
                        join u in this.database.Users
                        on d.UserId equals u.Id
                        where u.Username == username && d.Id == daemonId
                        select d;

            return query.First();
        }

        private int IdentifyBorder(string data)
        {
            string value = "";
            foreach (char item in data)
            {
                value += item;
                try
                {
                    if (value.Substring(value.Length - 10) == "@@border@@")
                        break;
                }
                catch { } 
            }

            return value.Length - 10;
        }

    }
}