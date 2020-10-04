using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ClienteTodoList
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //get the endpoints
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5003");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            //authorization
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client01",
                ClientSecret = "client01secret",
                Scope = "todolist"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            //request
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:5001/todo");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}
