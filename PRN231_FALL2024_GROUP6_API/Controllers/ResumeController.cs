using BusinessObject.AddModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        public IResumeService service = new ResumeService();
        public ResumeController()
        {
        }

        [HttpGet("View")]
        public async Task<IActionResult> ViewResume([FromQuery] int accountId)
        {
            var result = await service.ViewAllResumeAccount(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetailResume(int resumeId)
        {
            var result = await service.ViewDetailResume(resumeId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddResume([FromBody] ResumeAdd key)
        {
            var result = await service.AddResume(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
