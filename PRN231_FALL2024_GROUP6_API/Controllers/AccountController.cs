//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.Google;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;
//using System;
//using Service.Service;
//using Service.IService;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;
//using Microsoft.Extensions.Configuration;
//using System.Net.Mail;
//using System.Net;
//using Microsoft.AspNetCore.OData.Query;
//using BusinessObject.ViewModels;

//namespace IndieGameHubSever.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AccountController : ControllerBase
//    {
//        public IAccountService service = new AccountService();
//        private readonly IConfiguration _configuration;
//        public AccountController(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }
//        [HttpGet("ViewList")]
//        [EnableQuery]
//        public async Task<IActionResult> ViewListAccount(int sizePaging, int indexPaging)
//        {
//            var result = await service.ViewListAccount(sizePaging, indexPaging);
//            if (result.Status == 200) return Ok(result);
//            else return BadRequest(result);
//        }
//        [HttpPost("send")]
//        public async Task<IActionResult> SendEmail(string toEmail)
//        {
//            var result = await service.SendToken(toEmail);
//            if (result.Status != 200) return BadRequest(result);
//            var smtpSettings = _configuration.GetSection("Smtp");

//            var smtpClient = new SmtpClient(smtpSettings["Host"])
//            {
//                Port = int.Parse(smtpSettings["Port"]),
//                Credentials = new NetworkCredential(smtpSettings["UserName"], smtpSettings["Password"]),
//                EnableSsl = bool.Parse(smtpSettings["EnableSSL"])
//            };

//            var mailMessage = new MailMessage
//            {
//                From = new MailAddress(smtpSettings["UserName"]),
//                Subject = "Confirm Token",
//                Body = (String)result.Data,
//                IsBodyHtml = true,
//            };

//            mailMessage.To.Add(toEmail);

//            try
//            {
//                await smtpClient.SendMailAsync(mailMessage);
//                return Ok(result);
//            }
//            catch (SmtpException ex)
//            {
//                return StatusCode(500, $"Lỗi khi gửi email: {ex.Message}");
//            }
//        }
//        [HttpPost("resetPassword")]
//        public async Task<IActionResult> ResetPassword(string email, string password, string token)
//        {
//            var result = await service.ResetPassword(email, password, token);
//            if (result.Status == 200) return Ok(result);
//            else return BadRequest(result);
//        }
//        // Helper method to get the current user's profile
//        [NonAction]
//        private async Task<AccountInfo?> GetCurrentUserProfileAsync()
//        {
//            var userIdString = User.FindFirstValue(ClaimTypes.Sid); // Ensure this matches your claim type

//            if (Guid.TryParse(userIdString, out Guid userId))
//            {
//                return await _profileService.GetUserProfileAsync(userId);
//            }

//            throw new Exception("Authentication token is invalid or missing");
//        }

//    }
//}
