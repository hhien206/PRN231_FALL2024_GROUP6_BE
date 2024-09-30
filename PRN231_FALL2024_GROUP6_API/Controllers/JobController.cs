using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        public IJobService service = new JobService();
        public JobController()
        {
        }
        [HttpPost("ViewSearch")]
        public async Task<IActionResult> ViewJobSearch(int sizePaging, int indexPaging, [FromBody] JobSearch key)
        {
            var result = await service.ViewJobSearch(sizePaging, indexPaging, key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewJobDetail([FromQuery] int jobId)
        {
            var result = await service.ViewJobDetail(jobId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddJob([FromBody] JobAdd key)
        {
            var result = await service.AddJob(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
