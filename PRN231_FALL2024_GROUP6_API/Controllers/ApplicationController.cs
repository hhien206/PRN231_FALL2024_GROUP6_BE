using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;
using System.ComponentModel;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        public IApplicationService service = new ApplicationService();
        public ApplicationController()
        {
        }

        [HttpGet("ViewJob")]
        public async Task<IActionResult> ViewJob([FromQuery] int jobId)
        {
            var result = await service.ViewApplicationJob(jobId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewAccount")]
        public async Task<IActionResult> ViewAccount([FromQuery] int accountId)
        {
            var result = await service.ViewApplicationAccount(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetail([FromQuery] int applicationId)
        {
            var result = await service.ViewApplicationDetail(applicationId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ApplicationAdd key)
        {
            var result = await service.AddApplication(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPut("Accept")]
        public async Task<IActionResult> Accept([FromQuery] int applicationId)
        {
            var result = await service.UpdateApplicationStatus(new ApplicationUpdate
            {
                ApplicationId = applicationId,
                Status = "ACCEPTED"
            });
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPut("Refuse")]
        public async Task<IActionResult> Refuse([FromQuery] int applicationId)
        {
            var result = await service.UpdateApplicationStatus(new ApplicationUpdate
            {
                ApplicationId = applicationId,
                Status = "REFUSED"
            });
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPut("RefuseInsufficiant")]
        public async Task<IActionResult> RefuseInsufficiant([FromQuery] int jobId)
        {
            var result = await service.RefuseAllApplicationInsufficient(jobId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
