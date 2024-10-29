using BusinessObject.AddModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducateController : ControllerBase
    {
        public IEducateService service = new EducateService();
        public EducateController()
        {
        }

        [HttpGet("View")]
        public async Task<IActionResult> ViewEducate([FromQuery] int accountId)
        {
            var result = await service.ViewAllEducateAccount(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetailEducate(int educateId)
        {
            var result = await service.ViewDetailEducate(educateId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddEducate([FromBody] EducateAdd key)
        {
            var result = await service.AddEducate(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteEducate(int educateId)
        {
            var result = await service.DeleteEducate(educateId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
