using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LagRabbitMQ.Services
{
    public static class RequestService
    {
        public static HttpClient HttpCliente = new HttpClient();

        public static async Task<T> Get<T>(Uri url, string token) where T : class
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = url,
                Method = HttpMethod.Get
            };

            HttpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

            var httpResult = await HttpCliente.SendAsync(httpRequest);

            var result = await httpResult.Content.ReadAsStringAsync();

            if (!httpResult.IsSuccessStatusCode)
                throw new Exception(result);

            return JsonConvert.DeserializeObject<T>(result);
        }

        public static async Task<T> Post<T>(Uri url, string token, object body) where T : class
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = url,
                Method = HttpMethod.Post
            };

            var bodyJson = JsonConvert.SerializeObject(body);

            HttpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

            httpRequest.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

            var httpResult = await HttpCliente.SendAsync(httpRequest);

            var result = await httpResult.Content.ReadAsStringAsync();

            if (!httpResult.IsSuccessStatusCode)
                throw new Exception(result);

            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        ///     Gera token de autenticação da API do RabbitMQ
        /// </summary>
        public static string GetAuthToken(string user, string password) => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{password}"));
    }
}
