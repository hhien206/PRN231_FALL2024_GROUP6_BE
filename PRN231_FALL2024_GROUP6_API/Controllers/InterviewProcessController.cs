using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewProcessController : ControllerBase
    {
        public IInterviewProcessService service = new InterviewProcessService();
        public InterviewProcessController()
        {
        }

        [HttpGet("ViewUser")]
        public async Task<IActionResult> ViewUser([FromQuery] int accountId)
        {
            var result = await service.ViewListInterviewProcessUser(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewHr")]
        public async Task<IActionResult> ViewHr([FromQuery] int accountId)
        {
            var result = await service.ViewListInterviewProcessHR(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewJob")]
        public async Task<IActionResult> ViewJob([FromQuery] int jobId)
        {
            var result = await service.ViewListInterviewProcessJob(jobId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetail([FromQuery] int interviewProcessId)
        {
            var result = await service.ViewInterviewProcessDetail(interviewProcessId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] InterviewProcessAdd key)
        {
            var result = await service.InterviewProcessAdd(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] InterviewProcessUpdate key)
        {
            var result = await service.InterviewProcessUpdate(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
