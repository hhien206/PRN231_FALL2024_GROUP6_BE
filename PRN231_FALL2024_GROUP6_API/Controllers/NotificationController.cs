using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public INotificationService service = new NotificationService();
        public NotificationController()
        {
        }
        [HttpGet("ViewUnseenQuantity")]
        public async Task<IActionResult> ViewUnseenQuantity([FromQuery] int accountId)
        {
            var result = await service.ViewNotificationUnseenQuantity(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }

        [HttpGet("ViewUser")]
        public async Task<IActionResult> ViewUser([FromQuery] int accountId)
        {
            var result = await service.ViewListNotificationUser(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
