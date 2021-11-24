using DB_CSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API_CSharp.Controllers
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
        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] ProjectListSearchRequest request)
        {
            var result = await _service.Search(request);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObj);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] short id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.Delete(id, userId);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObj);
            }
            return BadRequest(result.Message);
        }
    }
}
