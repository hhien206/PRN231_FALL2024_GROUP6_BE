using BusinessObject.AddModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountJobSkillController : ControllerBase
    {
        public IAccountJobSkillService service = new AccountJobSkillService();
        public AccountJobSkillController()
        {
        }

        [HttpGet("View")]
        public async Task<IActionResult> ViewAccountJobSkill([FromQuery] int accountId)
        {
            var result = await service.ViewAllAccountJobSkillAccount(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetailAccountJobSkill(int AccountJobSkillId)
        {
            var result = await service.ViewDetailAccountJobSkill(AccountJobSkillId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAccountJobSkill([FromBody] AccountJobSkillAdd key)
        {
            var result = await service.AddAccountJobSkill(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAccountJobSkill([FromQuery] int accountJobSkillId)
        {
            var result = await service.DeleteAccountJobSkill(accountJobSkillId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
