using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Settings.Projects;
using System.Threading.Tasks;

namespace API_CSharp.Controllers.Projects
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectListController : ControllerBase
    {
        private readonly IProjectListService _service;
        public ProjectListController(IProjectListService projectService)
        {
            _service = projectService;
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search()
        {
            var result = await _service.Search();
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObj);
            }
            return BadRequest(result.Message);
        }
    }
}
