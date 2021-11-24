using DB_CSharp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API_CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDetailController : ControllerBase
    {
        private readonly IProjectDetailService _service;
        public ProjectDetailController(IProjectDetailService projectDetailService)
        {
            _service = projectDetailService;
        }
        [HttpGet("Init/{id:int}")]
        public async Task<IActionResult> Init([FromRoute]short id)
        {
            var result = await _service.Init(id);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObj);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("Save")]
        //[Authorize]
        public async Task<IActionResult> Save([FromBody]Project request)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.Save(request, userId);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObj);
            }
            return BadRequest(result.Message);
        }
    }
}
