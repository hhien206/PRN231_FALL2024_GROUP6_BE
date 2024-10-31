using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpGet("View")]
        public async Task<IActionResult> View()
        {
            try
            {
                string json = System.IO.File.ReadAllText("company.json");
                CompanyView data = JsonSerializer.Deserialize<CompanyView>(json);
                return Ok(new ServiceResult
                {
                    Status = 200,
                    Message = "Company",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResult
                {
                    Status = 501,
                    Message = ex.Message
                });
            }
        }
    }
}
