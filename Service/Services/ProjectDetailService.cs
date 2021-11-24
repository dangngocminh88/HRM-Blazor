using DB_CSharp;
using DB_CSharp.Entities;
using DB_CSharp.Models.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IProjectDetailService
    {
        Task<ApiResult<Project>> Init(long id);
        Task<ApiResult<int>> Save(Project request, string userId);
    }
    public class ProjectDetailService : IProjectDetailService
    {
        private readonly AppDBContext _context;
        public ProjectDetailService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        public async Task<ApiResult<Project>> Init(long id)
        {
            Project response = await _context.ProjectDB.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return new ApiSuccessResult<Project>(response);
        }
        public async Task<ApiResult<int>> Save(Project request, string userId)
        {
            if (request.Id != 0)
            {
                Project project = await _context.ProjectDB.AsNoTracking().FirstOrDefaultAsync(x =>
                    x.Id == request.Id && x.ChangeCount == request.ChangeCount);
                if (project == null)
                {
                    return new ApiErrorResult<int>("Data Changed On Server");
                }

                bool exists = await _context.ProjectDB.AsNoTracking().AnyAsync(x =>
                    x.Id != request.Id && x.ProjectName == request.ProjectName);
                if (exists)
                {
                    return new ApiErrorResult<int>("Project exists");
                }
            }
            request.ProjectName = request.ProjectName;
            request.Active = request.Active;
            request.ChangeDate = DateTime.Now;
            request.ChangeCount++;
            request.ChangeBy = userId;
            if (request.Id == 0)
            {
                _context.ProjectDB.Add(request);
            }
            else
            {
                _context.ProjectDB.Update(request);
            }
            int response = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(response);
        }
    }
}
