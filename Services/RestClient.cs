using System;
using System.Net.Http;
using Newtonsoft.Json;


namespace rubiera.Services
{
    public class RestClient
    {
        private readonly HttpClient client;

        public RestClient(HttpClient client)
        {
            this.client = client;
        }

        public T get<T>(string uri)
        {
            var task = client.GetStringAsync(uri);
            task.Wait();
            if (task.Exception != null)
                throw task.Exception;
            return JsonConvert.DeserializeObject<T>(task.Result);
        }
    }
}