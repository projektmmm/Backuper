using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace backuperApi.Controllers.Admin
{
    public class AdminSettings_Controller : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/admin/AdminSettings/{username}")]
        public string Get(string username)
        {
            string email = this.database.Users.Where(u => u.Username == username).First<Users>().Email;
            return email;
        }
        [HttpPatch]
        [Route("api/admin/AdminSettings/newEmail")]
        public bool ChangeEmail(AdminChange content)
        {
            Users user = this.database.Users.Where(u => u.Username == content.Username).First<Users>();
            if (BCrypt.Net.BCrypt.Verify(content.Password, user.Password)) //missing Bcrypt ==> BCrypt.Net.BCrypt.Verify(content.Password,user.Password))
            {
                user.Email = content.Content;
                this.database.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        [HttpPatch]
        [Route("api/admin/AdminSettings/newPassword")]
        public bool ChangePassword(AdminChange content)
        {
            Users user = this.database.Users.Where(u => u.Username == content.Username).First<Users>();
            if (BCrypt.Net.BCrypt.Verify(content.Password, user.Password)) //missing Bcrypt ==>  BCrypt.Net.BCrypt.Verify(content.Password,user.Password))
            {
                user.Password = content.Password;
                this.database.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        [HttpGet]
        [Route("api/admin/AdminSettings/SMS/{DaemonId}")]
        public void SMS(int DaemonId)
        {
            Daemons daemon = this.database.Daemons.Where(d => d.Id == DaemonId).First<Daemons>();

            if (daemon.SendSMS)
            {
                daemon.SendSMS = false;
            }
            else
            {
                daemon.SendSMS = true;
            }
            this.database.SaveChanges();
        }
        [HttpGet]
        [Route("api/admin/AdminSettings/Email/{DaemonId}")]
        public void Email(int DaemonId)
        {
            Daemons daemon = this.database.Daemons.Where(d => d.Id == DaemonId).First<Daemons>();
            if (daemon.SendEmail)
            {
                daemon.SendEmail = false;
            }
            else
            {
                daemon.SendEmail = true;
            }
            this.database.SaveChanges();
        }
    }
}