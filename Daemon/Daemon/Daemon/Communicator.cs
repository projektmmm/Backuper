using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class Communicator
    {
        /// <summary>
        /// Kontrola, zda neni nove nastaveni backupu
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
        /// Ziskani nastaveni pro komunikaci
        /// </summary>
        private static HttpClient GetJsonClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DaemonSettings.ApiAdress);
            client.DefaultRequestHeaders.Accept.Clear(); //odstraneni predeslych pozadovanych formatu odpovedi
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //chci dostat zpet json

            return client;
        }
    }
}
