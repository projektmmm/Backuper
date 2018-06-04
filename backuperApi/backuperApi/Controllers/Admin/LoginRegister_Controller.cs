using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
                Users use = this.database.Users.Where(u => (u.Username == user.Username || u.Email == user.Username)).First<Users>();

                if (!(user.Username == "Vojtech" || user.Username == "Daniel"))
                {
                    if (BCrypt.Net.BCrypt.Verify(user.Password, use.Password))
                        return this.PostFind(user.Username);
                    else
                        return "false";
                }
                else
                {
                    if (use.Password == user.Password)
                        return this.PostFind(user.Username);
                    else
                        return "false";
                }
            }
            catch
            {
                return "false";
            }
        }

        private string CreateToken(string username)
        {
            DateTime issuedAt = DateTime.Now;
            //set the time when it expires
            DateTime expires = DateTime.Now.AddDays(7);

            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.Now;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:63324", audience: "http://localhost:63324",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString + "@@border@@" + username;
        }

        [HttpPost]
        [Route("api/admin/LocalStorage")]
        private string PostFind(string data)
        {
            Users user = this.database.Users.Where((u => (u.Username == data || u.Email == data))).First();


            string token = this.CreateToken(user.Username);
            return token;
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