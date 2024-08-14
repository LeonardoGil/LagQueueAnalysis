using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LagRabbitMQ.Services
{
    // Mudar
    public static class RequestServices
    {
        public static HttpClient HttpCliente = new HttpClient();

        public static async Task<T> Get<T>(string url, string token) where T : class
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            HttpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

            var httpResult = await HttpCliente.SendAsync(httpRequest);

            var result = await httpResult.Content.ReadAsStringAsync();

            if (!httpResult.IsSuccessStatusCode)
                throw new Exception(result);

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
