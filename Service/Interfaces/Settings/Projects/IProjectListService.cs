using DB_CSharp.Entities;
using DB_CSharp.Models.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces.Settings.Projects
{
    public interface IProjectListService
    {
        Task<ApiResult<List<Project>>> Search();
    }
}
