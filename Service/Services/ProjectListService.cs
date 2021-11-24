using DB_CSharp;
using DB_CSharp.Entities;
using DB_CSharp.Models;
using DB_CSharp.Models.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IProjectListService
    {
        Task<ApiResult<List<Project>>> Search(ProjectListSearchRequest request);
        Task<ApiResult<int>> Delete(short id, string userId);
    }
    public class ProjectListService : IProjectListService
    {
        private readonly AppDBContext _context;
        public ProjectListService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        public async Task<ApiResult<List<Project>>> Search(ProjectListSearchRequest request)
        {
            try
            {
                var query = _context.ProjectDB.AsNoTracking().AsQueryable();
                if (!string.IsNullOrEmpty(request.ProjectName))
                {
                    query = query.Where(x => x.ProjectName == request.ProjectName);
                }
                if (request.Active <= 1)
                {
                    query = query.Where(x => x.Active == Convert.ToBoolean(request.Active));
                }
                List<Project> projectList = await query.ToListAsync();
                return new ApiSuccessResult<List<Project>>(projectList);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<List<Project>>(ex.Message);
            }
        }
        public async Task<ApiResult<int>> Delete(short id, string userId)
        {
            Project project = new() { Id = id };
            _context.ProjectDB.Remove(project);
            int response = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(response);
        }
    }
}
