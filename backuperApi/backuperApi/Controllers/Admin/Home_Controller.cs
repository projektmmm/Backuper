using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace backuperApi.Controllers
{
    public class Home_Controller : ApiController
    {
        public Database database = new Database();
        [HttpGet]
        [Route("api/admin/Home/{username}")]
        public List<BackupErrors> GetProblems(string username)
        {
            int UserId = FindByUsername(username);
            var query = from d in this.database.BackupReport
                        join u in this.database.BackupErrors
                        on d.Id equals u.BackupReportId
                        join b in this.database.Backups
                        on d.BackupId equals b.Id
                        join s in this.database.Daemons
                        on b.DaemonId equals s.Id
                        where s.UserId == UserId && u.Solved == false
                        select u;

            return query.ToList<BackupErrors>();
        }
        [HttpGet]
        [Route("api/admin/Home/Daemon/{ReportId}")]
        public int GetDaemon(int ReportId)
        {

            var query = from d in this.database.BackupReport
                        join u in this.database.Backups
                        on d.BackupId equals u.Id
                        where d.Id == ReportId
                        select u.DaemonId;
            return query.First();
        }
        private int FindByUsername(string username)
        {
            Users user = this.database.Users.Where(u => u.Username == username).First<Users>();
            return user.Id;
        }
    }
}