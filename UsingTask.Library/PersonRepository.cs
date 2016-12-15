using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using UsingTask.Shared;

namespace UsingTask.Library
{
    public class PersonProgressData
    {
        public int Item { get; }
        public int Total { get; }
        public string Name { get; }

        public PersonProgressData(int item, int total, string name)
        {
            Item = item;
            Total = total;
            Name = name;
        }
    }

    public class PersonRepository
    {
        public async Task<List<Person>> Get(
            IProgress<PersonProgressData> progress,
            CancellationToken cancellationToken = new CancellationToken())
        {
            //await Task.Delay(3000);

            cancellationToken.ThrowIfCancellationRequested();

            // Uncomment to test exception handling in calling code
            //throw new NotImplementedException("Get operation not implemented");

            using (var client = new HttpClient())
            {
                InitializeClient(client);
                HttpResponseMessage response = await client.GetAsync("api/people", cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var people = await response.Content.ReadAsAsync<List<Person>>();
                    for (int i = 0; i < people.Count; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        progress?.Report(new PersonProgressData(i + 1, people.Count,
                            people[i].ToString()));
                        await Task.Delay(300);
                    }
                    return people;
                }
                return new List<Person>();
            }
        }

        public async Task<Person> Get(int id)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);
                HttpResponseMessage response = await client.GetAsync("api/people/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Person>();
                }
                return null;
            }
        }

        private static void InitializeClient(HttpClient client)
        {
            client.BaseAddress = new Uri("http://localhost:9874/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
