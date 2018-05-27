using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        /// Odeslani reportu o backupu
        /// </summary>
        public async Task PostBackupReport(BackupReport report, List<ErrorDetails> errorDetails)
        {
            string data = JsonConvert.SerializeObject(report) + "@@border@@" + JsonConvert.SerializeObject(errorDetails);
            string apiDestination = $"api/daemon/{this.daemonId.ToString()}";

            using (var client = GetJsonClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiDestination, data);
            }
        }

        public async Task GetFtpSettings()
        {
            string apiDestination = $"api/daemon/ftp/{this.daemonId.ToString()}";

            List<FtpSettings> ret = new List<FtpSettings>();
            using (var client = GetJsonClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiDestination);
                if (response.IsSuccessStatusCode)
                {
                    DaemonSettings.ftpSettings = await response.Content.ReadAsAsync<List<FtpSettings>>();
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
