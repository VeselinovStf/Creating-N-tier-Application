using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PluralSightBook.CLI.WebAPIService
{
    public class ApiConfig
    {
        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44353/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}