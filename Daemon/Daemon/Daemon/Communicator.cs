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
        int daemonId = DaemonSettings.Id;

        /// <summary>
        /// Kontrola, zda neni nove nastaveni backupu
        /// </summary>
        public async Task GetNextRunSetting(string apiDestination = "")
        {
            if (apiDestination == "")
                apiDestination = $"api/daemon/{this.daemonId.ToString()}";

            List<PlannedBackups> ret = new List<PlannedBackups>();
            using (var client = GetJsonClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiDestination);
                if (response.IsSuccessStatusCode)
                {
                    DaemonSettings.plannedBackups = await response.Content.ReadAsAsync<List<PlannedBackups>>();
                }
                else
                {
                    throw new HttpRequestException("Cannot connect to the api");
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
