using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Admin
{
    public class ApiCommunication
    {
        public static Uri Adress { get; set; } = new Uri("http://localhost:54736/");
        private static string Key { get; set; } = "5156badb-b49f-4687-8af8-448c7f3f7688";
        private GlobalSettings globalSettings = new GlobalSettings();

        /// <summary>
        /// Odeslani reportu o Backupu
        /// </summary>
        public async Task PostBackupReport(List<string> toPost, string apiDestination)
        {
            JwtHeader header = ApiCommunication.CreateToken();

            JwtPayload content = new JwtPayload
            {
                { "AdminId", this.globalSettings.AdminId },
                { "Password", this.globalSettings.GetHashedPassword() }
            };

            JwtSecurityToken secToken = new JwtSecurityToken(header, content);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = handler.WriteToken(secToken);
            toPost.Add(token);

            using (var client = GetJsonClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiDestination, toPost);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }

        /// <summary>
        /// Odesilane a prijimane informace budou vyzadovany v json formatu
        /// </summary>
        private static HttpClient GetJsonClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = ApiCommunication.Adress;
            client.DefaultRequestHeaders.Accept.Clear(); //odstraneni predeslych pozadovanych formatu odpovedi
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //chci dostat zpet json

            return client;
        }

        /// <summary>
        /// Vytvori hlavicku tokenu
        /// </summary>
        private static JwtHeader CreateToken()
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiCommunication.Key));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            return new JwtHeader(credentials);
        }
    }
}
