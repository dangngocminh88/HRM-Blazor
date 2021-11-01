using DB_CSharp.Models.Commons;
using DB_CSharp.Models.Users;
using System.Threading.Tasks;

namespace Service.Interfaces.Users
{
    public interface IUserService
    {
        Task<ApiResult<bool>> Register(UserRegisterRequest request);
        Task<ApiResult<string>> Login(UserLoginRequest request);
    }
}
