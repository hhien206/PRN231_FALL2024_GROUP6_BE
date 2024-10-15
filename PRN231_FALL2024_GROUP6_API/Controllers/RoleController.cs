using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IRoleService service = new RoleService();
        public RoleController()
        {
        }

        [HttpGet("View")]
        public async Task<IActionResult> ViewRole()
        {
            var result = await service.ViewListRole();
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetailRole(int roleId)
        {
            var result = await service.ViewRoleDetail(roleId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
