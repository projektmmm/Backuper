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
            return this.database.Backups.Where(b => b.NextRun > DateTime.Now).First<Backups>();
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