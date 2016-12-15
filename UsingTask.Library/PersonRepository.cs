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
    public class PersonRepository
    {
        public async Task<List<Person>> Get(
            IProgress<int> progress,
            CancellationToken cancellationToken = new CancellationToken())
        {
            progress?.Report(3);
            await Task.Delay(1000, cancellationToken);

            progress?.Report(2);
            await Task.Delay(1000, cancellationToken);

            progress?.Report(1);
            await Task.Delay(1000, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            // Uncomment to test exception handling in calling code
            //throw new NotImplementedException("Get operation not implemented");

            using (var client = new HttpClient())
            {
                InitializeClient(client);
                HttpResponseMessage response = await client.GetAsync("api/people", cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    progress?.Report(0);
                    return await response.Content.ReadAsAsync<List<Person>>();
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
