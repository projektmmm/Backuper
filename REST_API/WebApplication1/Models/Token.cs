using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Models
{
    public class Token
    {
        public int IdAdmin { get; set; }
        public string Password { get; set; }
        private readonly string Key = "5156badb-b49f-4687-8af8-448c7f3f7688";

        public Token()
        {
        }

        public bool Verify(string encryptedToken)
        {
            SecurityToken validatedToken = null;
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(encryptedToken, this.GetValidationParameters(), out validatedToken);
            Token token = new Token();

            string validatedStringToken = validatedToken.ToString();
            for (int i = 1; i < validatedStringToken.Length; i++)
            {
                if (validatedStringToken[i - 1] == '}' && validatedStringToken[i] == '.' && validatedStringToken[i + 1] == '{')
                {
                    token = JsonConvert.DeserializeObject<Token>(validatedStringToken.Substring(++i));
                    break;
                }
            }

            this.IdAdmin = token.IdAdmin;
            token.Password = token.Password;         

            string commandText = "SELECT * FROM tbAdminTokens WHERE IdAdmin=@IdAdmin AND Password=@Password";
            bool ret = false;
            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                MySqlCommand command = new MySqlCommand(commandText, sConn);

                command.Parameters.Add("@IdAdmin", MySqlDbType.Int32);
                command.Parameters.Add("@Password", MySqlDbType.VarChar);
                command.Parameters["@IdAdmin"].Value = this.IdAdmin;
                command.Parameters["@Password"].Value = this.Password;

                MySqlDataReader sRead = command.ExecuteReader();

                if (sRead.Read())
                {
                    if (Convert.ToInt32(sRead[0]) == this.IdAdmin && sRead[1].ToString() == this.Password)
                        ret = true;
                    else
                        ret = false;
                }
                else
                    ret = false;                

                sConn.Close();
            }

            return ret;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key)),
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true
            };
        }
    }
}