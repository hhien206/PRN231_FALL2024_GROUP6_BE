using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : ControllerBase
    {
        public IJobTypeService service = new JobTypeService();
        public JobTypeController()
        {
        }

        [HttpGet("View")]
        public async Task<IActionResult> ViewJobType([FromQuery] string? nameQuickSearch)
        {
            var result = await service.ViewAllJobType(nameQuickSearch);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetailJobType(int jobTypeId)
        {
            var result = await service.ViewDetailJobType(jobTypeId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddJobType([FromBody] JobTypeAdd key)
        {
            var result = await service.AddJobType(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateJobType(int jobTypeId, [FromBody] JobTypeUpdate key)
        {
            var result = await service.UpdateJobType(jobTypeId, key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteJobType(int jobTypeId)
        {
            var result = await service.DeleteJobType(jobTypeId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
