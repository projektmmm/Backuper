using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace backuperApi.Controllers
{
    public class Daemons_Controller : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/admin/daemons/{username}")]
        public List<Daemons> Get(string username)
        {
            var query = from d in this.database.Daemons
                        join u in this.database.Users
                        on d.UserId equals u.Id
                        where u.Username == username
                        select d;

            return query.ToList<Daemons>();
        }

        [HttpGet]
        [Route("api/admin/daemons/{username}-{daemonId}")]
        public Daemons Get(string username, int daemonId)
        {
            var query = from d in this.database.Daemons
                        join u in this.database.Users
                        on d.UserId equals u.Id
                        where u.Username == username && d.Id == daemonId
                        select d;

            return query.First();
        }
        [HttpPost]
        [Route("api/admin/daemons/AddDaemon")]
        public string Post(Daemons daemon)
        {
            Daemons ToAdd = daemon;
            try
            {
                this.database.Daemons.Add(ToAdd);
                this.database.SaveChanges();
                //SendMail
                //Make settings.txt
                return "Daemon Added";
            }
            catch
            {
                return "OOPS, error on our side...";
            }
        }
        [HttpPost]
        [Route("api/admin/daemons/GetId")]
        public int GetId(Users Username)
        {
            Users user = this.database.Users.Where(u => u.Username == Username.Username).First<Users>();
            return user.Id;
        }

        [HttpPatch]
        [Route("api/admin/daemons/AddDaemon/{daemonId}")]
        public bool patch(int daemonId)
        {
            Daemons dbRecord = this.FindById(daemonId);

            dbRecord.Verified = true;
            this.database.SaveChanges();
            return true;
        }
        [HttpDelete]
        [Route("api/admin/daemons/DeleteDaemon/{id}")]
        public bool Delete(int id)
        {
            Daemons del = this.FindById(id);
            this.database.Daemons.Remove(del);

            this.database.SaveChanges();
            return true;
        }

        private Daemons FindById(int id)
        {
            return this.database.Daemons.Find(id);
        }
        [HttpGet]
        [Route("api/admin/daemons/AddDaemon/{username}")]
        public List<Daemons> GetDaemonRequest(string username)
        {
            var query = from d in this.database.Daemons
                        join u in this.database.Users
                        on d.UserId equals u.Id
                        where u.Username == username && d.Verified == false && d.Installed == true
                        select d;

            return query.ToList<Daemons>();
        }
        [HttpGet]
        [Route("api/admin/daemons/Get/{username}")]
        public bool GetRequests(string username)
        {
            var query = from d in this.database.Daemons
                        join u in this.database.Users
                        on d.UserId equals u.Id
                        where u.Username == username && d.Verified == false && d.Installed == true
                        select d;

            List<Daemons> List = query.ToList<Daemons>();
            try
            {
                Daemons a = List[0];
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}