using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace backuperApi.Controllers
{
    public class LoginRegister_Controller : ApiController
    {
        public Database database = new Database();

        [HttpPost]
        [Route("api/admin/register")]
        public bool Post(Users user)
        {
            bool test = this.FindByUsername(user.Username);
            if (test != false)
                return false;

            this.database.Users.Add(user);
            this.database.SaveChanges();
            return true;
        }

        [HttpPost]
        [Route("api/admin/login")]
        public bool PostLogin(Users user)
        {
            try
            {
                this.database.Users.Where(u => u.Username == user.Username && u.Password == user.Password).First<Users>();
                return true;
            }
            catch
            {
                return false;
            }
        }


        private bool FindByUsername(string username)
        {
            //return this.database.Users.Find(username);
            try
            {
                this.database.Users.Where(u => u.Username == username).First<Users>();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}