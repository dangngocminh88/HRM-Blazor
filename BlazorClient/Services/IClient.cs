using DB_CSharp.Models.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClient.Services
{
    public interface IClient
    {
        public Task<ApiResult<List<T>>> GetListAsync<T>(string url);
    }
}
