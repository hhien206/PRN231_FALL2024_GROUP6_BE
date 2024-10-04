using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobLevelsController : ControllerBase
    {
        public IJobLevelService service = new JobLevelService();
        public JobLevelsController()
        {
        }

        [HttpGet("View")]
        public async Task<IActionResult> ViewJobLevel([FromQuery] string? nameQuickSearch)
        {
            var result = await service.ViewAllJobLevel(nameQuickSearch);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetailJobLevel(int jobLevelId)
        {
            var result = await service.ViewDetailobLevel(jobLevelId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddJobLevel([FromBody] JobLevelAdd key)
        {
            var result = await service.AddJobLevel(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateJobLevel(int jobLevelId, [FromBody] JobLevelUpdate key)
        {
            var result = await service.UpdateJobLevel(jobLevelId, key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteJobLevel(int jobLevelId)
        {
            var result = await service.DeleteJobLevel(jobLevelId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
