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
        public int AdminId { get; set; }
        public string Password { get; set; }
        private readonly string Key = "5156badb-b49f-4687-8af8-448c7f3f7688";

        /// <summary>
        /// Zjisti, zda jsou prihlasovaci udaje platne a token se naleza v databazi
        /// </summary>
        public bool Verify(string encryptedToken)
        {
            SecurityToken validatedToken = null;
            JwtSecurityToken token = null;

            //zjisti, zda token nekdo neoklamal
            try
            {
                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(encryptedToken, this.GetValidationParameters(), out validatedToken);
                token = new JwtSecurityTokenHandler().ReadJwtToken(encryptedToken);
            }
            catch
            {
                return false;
            }

            this.AdminId = Convert.ToInt32(token.Payload["AdminId"]);
            this.Password = token.Payload["Password"].ToString();


            string commandText = "SELECT * FROM tbAdminTokens WHERE IdAdmin=@IdAdmin AND Password=@Password";
            bool ret = false;
            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                MySqlCommand command = new MySqlCommand(commandText, sConn);

                command.Parameters.Add("@IdAdmin", MySqlDbType.Int32);
                command.Parameters.Add("@Password", MySqlDbType.VarChar);
                command.Parameters["@IdAdmin"].Value = this.AdminId;
                command.Parameters["@Password"].Value = this.Password;

                MySqlDataReader sRead = command.ExecuteReader();

                if (sRead.Read())
                {
                    if (Convert.ToInt32(sRead[0]) == this.AdminId && sRead[1].ToString() == this.Password)
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

        /// <summary>
        /// Ziskani validacnich parametru pro overovani tokenu
        /// </summary>
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