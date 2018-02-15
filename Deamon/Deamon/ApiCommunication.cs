using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Daemon
{
    public static class ApiCommunication
    {
        public static Uri Adress { get; set; } = new Uri("http://localhost:54736/");

        public static async Task PostBackupReport(List<string> toPost, string apiDestination)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = ApiCommunication.Adress;
                client.DefaultRequestHeaders.Accept.Clear(); //odstraneni predeslych pozadovanych formatu odpovedi
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //chci dostat zpet json

                HttpResponseMessage response = await client.PostAsJsonAsync(apiDestination, toPost);
            }
        }
    }
}
