using DB_CSharp.Entities;
using DB_CSharp.Models.Commons;
using DB_CSharp.Models.Projects;
using System.Threading.Tasks;

namespace Service.Interfaces.Settings.Projects
{
    public interface IProjectDetailService
    {
        Task<ApiResult<Project>> Init(long id);
        Task<ApiResult<int>> Save(Project request, string userId);
    }
}
