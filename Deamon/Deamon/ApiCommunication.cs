using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Deamon
{
    public class ApiCommunication
    {
        public List<FileInfo> ToPost { get; set; }

        public async void Post()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = Connection.Api;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.PostAsJsonAsync("api/person", this.ToPost);
            }
        }
    }
}
