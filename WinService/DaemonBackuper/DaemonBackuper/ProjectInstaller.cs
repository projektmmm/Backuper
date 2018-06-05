using Daemon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace DaemonBackuper
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            this.IsInstalled();
            new ServiceController(serviceInstaller1.ServiceName).Start();
        }

        private async void IsInstalled()
        {
            string apiDestination = $"api/daemon/IsInstalled/{DaemonSettings.Id}";

            List<PlannedBackups> ret = new List<PlannedBackups>();
            using (var client = GetJsonClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiDestination);
                if (response.IsSuccessStatusCode)
                {
                    
                }
                else
                {
                    throw new HttpRequestException("Cannot connect to the api");
                }
            }
        }
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
