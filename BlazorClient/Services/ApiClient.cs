using DB_CSharp.Models.Commons;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorClient.Services
{
    public interface IApiClient
    {
        Task<ApiResult<T>> GetAsync<T>(string url);
        Task<ApiResult<TResponse>> PostAsync<TResponse, TRequest>(string url, TRequest request);
    }
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly SpinnerService _spinnerService;
        public ApiClient(HttpClient httpClient, SpinnerService spinnerService)
        {
            _httpClient = httpClient;
            _spinnerService = spinnerService;
        }
        public async Task<ApiResult<T>> GetAsync<T>(string url)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(url);
            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                T result = JsonConvert.DeserializeObject<T>(response);
                return new ApiSuccessResult<T>(result);
            }
            else
            {
                return new ApiErrorResult<T>(response);
            }
        }
        public async Task<ApiResult<TResponse>> PostAsync<TResponse, TRequest>(string url, TRequest request)
        {
            _spinnerService.Show();
            await Task.Delay(5000);
            var result = await _httpClient.PostAsJsonAsync(url, request);
            _spinnerService.Hide();
            if (result.StatusCode == HttpStatusCode.OK)
            {
                TResponse response = await result.Content.ReadFromJsonAsync<TResponse>();
                return new ApiSuccessResult<TResponse>(response);
            }
            else
            {
                string response = await result.Content.ReadFromJsonAsync<string>();
                return new ApiErrorResult<TResponse>(response);
            }
        }
    }
}
