using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    /// <summary>
    /// Priklady, jak pouzivat rest api - GET, POST, PUT, DELETE
    /// Komunikace pomoci jsonu
    /// Nugetky:
    ///     Newtonsoft.Json
    ///     Microsoft.AspNet.WebApi.Client
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();

            Console.ReadLine();
        }

        /// <summary>
        /// Asynchronni volani rest api, obsahuje vsechny metody
        /// </summary>
        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                #region GET
                client.BaseAddress = new Uri("http://localhost:54736/");
                client.DefaultRequestHeaders.Accept.Clear(); //odstraneni predeslych pozadovanych formatu odpovedi
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //chci dostat zpet json

                Console.WriteLine("GET");
                HttpResponseMessage response = await client.GetAsync("api/person/3");

                if (response.IsSuccessStatusCode)
                {
                    Person person = await response.Content.ReadAsAsync<Person>();
                    Console.WriteLine($"{person.Id}\t{person.Name}\t{person.Surname}\t{person.Age}");
                }
                #endregion

                
                
                #region GETALL
                Console.WriteLine("GET ALL");
                response = await client.GetAsync("api/person");

                if (response.IsSuccessStatusCode)
                {
                    List<Person> person = await response.Content.ReadAsAsync<List<Person>>();

                    for (int i = 0; i < person.Count; i++)
                    {
                        Console.WriteLine($"{person[i].Id}\t{person[i].Name}\t{person[i].Surname}\t{person[i].Age}");
                    }
                }

                #endregion
                
                #region POST
                Console.WriteLine("POST");
                Person toPost = new Person()
                {
                    Id = 4,
                    Name = "Alois",
                    Surname = "Kedlubna",
                    Age = 18
                };

                response = await client.PostAsJsonAsync("api/person", toPost);
                if (response.IsSuccessStatusCode)
                {
                    Uri urll = response.Headers.Location;
                    Console.WriteLine(urll);
                }
                Uri url = response.Headers.Location;



                #endregion

                #region PUT

                Console.WriteLine("PUT");
                toPost.Surname = "Zeli";

                response = await client.PostAsJsonAsync(url, toPost);

                #endregion

                #region DELETE

                Console.WriteLine("DELETE");
                response = await client.DeleteAsync(url);

                #endregion

    
            }
        }
    }
}
