using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public static class ApiCommunication
    {
        public static Uri Adress { get; set; } = new Uri("http://localhost:54736/");

        /// <summary>
        /// Odeslani reportu o Backupu
        /// </summary>
        public static async Task PostBackupReport(List<string> toPost, string apiDestination)
        {
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

    }
}
