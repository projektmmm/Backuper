using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Daemon
{
    public class ApiCommunication
    {
        public static Uri Adress { get; set; } = new Uri("http://localhost:54736/");
        public NextRunSettings nextRunSettings;

        /// <summary>
        /// Odeslani reportu o Backupu
        /// </summary>
        public static async Task PostBackupReport(List<string> toPost, string apiDestination)
        {
            using (var client = GetJsonClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiDestination, toPost);
            }
        }

        /// <summary>
        /// Poptavka po novych nastavenich z databaze
        /// </summary>
        public async Task GetNextRunSetting(string apiDestination)
        {
            using (var client = GetJsonClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiDestination);
                if (response.IsSuccessStatusCode)
                {
                    this.nextRunSettings = new NextRunSettings();
                    this.nextRunSettings = await response.Content.ReadAsAsync<NextRunSettings>();
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
