using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        public ICertificateService service = new CertificateService();
        public CertificateController()
        {
        }

        [HttpGet("View")]
        public async Task<IActionResult> ViewCertificate([FromQuery] int accountId)
        {
            var result = await service.ViewAllCertificateAccount(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetailCertificate(int certificateId)
        {
            var result = await service.ViewDetailCertificate(certificateId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCertificate([FromBody] CertificateAdd key)
        {
            var result = await service.AddCertificate(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCertificate(int certificateId)
        {
            var result = await service.DeleteCertificate(certificateId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
    }
}
