using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BCrypt;


namespace backuperApi.Controllers
{
    public class LoginRegister_Controller : ApiController
    {
        public Database database = new Database();

        [HttpPost]
        [Route("api/admin/register")]
        public bool Post(Users user)
        {
            bool test = this.FindByUsername(user.Username) || this.FindByEmail(user.Email);
            if (test != false)
                return false;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password,salt);
            this.database.Users.Add(user);
            this.database.SaveChanges();
                return true;
          
        }

        [HttpPost]
        [Route("api/admin/login")]
        public string PostLogin(Users user)
        {
            try
            {
                this.database.Users.Where(u => (u.Username == user.Username||u.Email == user.Username) && BCrypt.Net.BCrypt.Verify(user.Password, u.Password));

                return this.PostFind(user.Username);
                
            }
            catch
            {
                return "false";
            }
        }
        [HttpPost]
        [Route("api/admin/LocalStorage")]
        private string PostFind(string data)
        {
            Users user = this.database.Users.Where((u => (u.Username == data || u.Email == data))).First();

            

            return user.Username;
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
        private bool FindByEmail(string email)
        {
            //return this.database.Users.Find(username);
            try
            {
                this.database.Users.Where(u => u.Email == email).First<Users>();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}