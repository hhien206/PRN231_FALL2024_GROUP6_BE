using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoryController : ControllerBase
    {
        public IJobCategoryService service = new JobCategoryService();
        public JobCategoryController()
        {
        }

        [HttpGet("View")]
        public async Task<IActionResult> ViewJobCategory([FromQuery] string? nameQuickSearch)
        {
            var result = await service.ViewAllJobCategory(nameQuickSearch);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetailJobCategory(int jobCategoryId)
        {
            var result = await service.ViewJobCategoryById(jobCategoryId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddJobCategory([FromBody] JobCategoryAdd key)
        {
            var result = await service.AddJobCategory(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateJobCategory(int jobCategoryId, [FromBody] JobCategoryUpdate key)
        {
            var result = await service.UpdateJobCategory(jobCategoryId, key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteJobCategory(int jobCategoryId)
        {
            var result = await service.DeleteJobCategory(jobCategoryId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
