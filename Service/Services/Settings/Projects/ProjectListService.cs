using DB_CSharp;
using DB_CSharp.Entities;
using DB_CSharp.Models.Commons;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Settings.Projects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Settings.Projects
{
    public class ProjectListService : IProjectListService
    {
        private readonly AppDBContext _context;
        public ProjectListService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        public async Task<ApiResult<List<Project>>> Search()
        {
            try
            {
                List<Project> projectList = await _context.ProjectDB.AsNoTracking().ToListAsync();
                return new ApiSuccessResult<List<Project>>(projectList);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<List<Project>>(ex.Message);
            }
        }
    }
}
