//using BusinessObject.ViewModel;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Service.IService;
//using Service.Service;

//namespace PRN231_FALL2024_GROUP6_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class JobSkillController : ControllerBase
//    {
//        public IJobSkillService service = new JobSkillService();
//        public JobSkillController()
//        {
//        }

//        [HttpGet("View")]
//        public async Task<IActionResult> ViewJobSkill([FromQuery] string? nameQuickSearch)
//        {
//            var result = await service.ViewAllJobSkill(nameQuickSearch);
//            if (result.Status == 200) return Ok(result);
//            else return BadRequest(result);
//        }

//        [HttpPost("Add")]
//        public async Task<IActionResult> AddJobSkill([FromBody] JobSkillAdd key)
//        {
//            var result = await service.AddJobSkill(key);
//            if (result.Status == 200) return Ok(result);
//            else return BadRequest(result);
//        }
//        [HttpPut("Update")]
//        public async Task<IActionResult> UpdateJobSkill(int jobSkillId, [FromBody] JobSkillUpdate key)
//        {
//            var result = await service.UpdateJobSkill(jobSkillId, key);
//            if (result.Status == 200) return Ok(result);
//            else return BadRequest(result);
//        }
//        [HttpDelete("Delete")]
//        public async Task<IActionResult> DeleteJobSkill(int jobSkillId)
//        {
//            var result = await service.DeleteJobSkill(jobSkillId);
//            if (result.Status == 200) return Ok(result);
//            else return BadRequest(result);
//        }
//    }
//}
