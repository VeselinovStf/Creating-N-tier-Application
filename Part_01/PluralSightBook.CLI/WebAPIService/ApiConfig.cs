using System.Net;

namespace PluralSightBook.CLI.WebAPIService
{
    public class ApiConfig
    {
        public static WebClient GetClient()
        {
            WebClient client = new WebClient();

            client.Headers.Add("Content-Type:application/json"); //Content-Type
            client.Headers.Add("Accept:application/json");
            client.BaseAddress = "https://localhost:44353";

            return client;
        }
    }
}