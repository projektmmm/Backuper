﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using backuperApi.Models;
using Newtonsoft.Json;

namespace backuperApi.Controllers.Daemon
{
    public class Daemon_Controller : ApiController
    {
        private Database database = new Database();
        private CronController cronController = new CronController();

        [HttpGet]
        [Route("api/daemon/{daemonId}")]
        public List<Backups> Get(int daemonId)
        {
            this.cronController.UpdateCron(this.database.Backups.Where(b => b.DaemonId == daemonId).ToList<Backups>());
            List<Backups> toRet = this.database.Backups.Where(b => b.DaemonId == daemonId).ToList<Backups>();

            return toRet;
<<<<<<< HEAD
        }
        [HttpGet]
        [Route("api/daemon/IsInstalled/{daemonId}")]
        public bool IsInstalled(int daemonId)
        {
            Daemons d = this.database.Daemons.Where(b => b.Id == daemonId).First();
            d.Installed = true;
            return true;
        }
        [HttpGet]
        [Route("api/daemon/GetDatabases/{daemonId}")]
        public List<Databases> GetDatabases(int daemonId)
        {
            return this.database.Databases.Where(d => d.DaemonId == daemonId).ToList<Databases>();
=======
>>>>>>> 4ec0bac1a3e487221c71023e912948ae9952ed08
        }
        [HttpPost]
        [Route("api/daemon/{daemonId}")]
        public void Post(int daemonId, [FromBody] string data)
        {
            Mail m = new Mail();
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

            Daemons dem = this.database.Daemons.Where(d => d.Id == daemonId).First();
            Users user = this.database.Users.Where(u => u.Id == dem.UserId).First();
            m.SendMail("Backup Report", tosub, user.Email);


            BackupReport dbRecord = this.database.BackupReport.Where(r => r.BackupId == report.BackupId && r.Date == report.Date && r.Size == report.Size && r.Type == report.Type).FirstOrDefault();
            int reportId = dbRecord.Id;
            List<BackupErrors> err = new List<BackupErrors>();

            for (int i = 0; i < errors.Count; i++)
            {
                bool add = true;
                BackupErrors error = errors[i];

                for (int j = 0; j < err.Count; j++)
                {
                    if (err[j].ProblemPath == error.ProblemPath && err[j].Problem == error.Problem)
                        add = false;
                }

                if (add)
                    err.Add(errors[i]);
            }

            foreach (BackupErrors item in err)
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