using DB_CSharp.Models.Commons;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorClient.Services
{
    public class Client : IClient
    {
        public HttpClient httpClient;
        public Client(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ApiResult<List<T>>> GetListAsync<T>(string url)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                List<T> result = JsonConvert.DeserializeObject<List<T>>(response);
                return new ApiSuccessResult<List<T>>(result);
            }
            else
            {
                return new ApiErrorResult<List<T>>(response);
            }
        }
    }
}
