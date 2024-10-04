using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Service.Service;
using Service.IService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.OData.Query;
using DataAccessObject.ViewModel;

namespace IndieGameHubSever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountService service = new AccountService();
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("ViewList")]
        [EnableQuery]
        public async Task<IActionResult> ViewListAccount(int sizePaging,int indexPaging)
        {
            var result = await service.ViewListAccount(sizePaging, indexPaging);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("Login")]
        public async Task<IActionResult> LoginAccount(string email, string password)
        {
            var result = await service.LoginAccount(email, password);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        /*[HttpGet("LoginToken")]
        public async Task<IActionResult> LoginToken(string token)
        {
            var result = await service.LoginToken(token);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }*/
        [HttpGet("Register")]
        public async Task<IActionResult> Register(string email,string password)
        {
            var token = GenerateJwtToken(email);
            var result = await service.AddAccount(new AccountAdd
            {
                userName = email,
                password = password,
                accessToken = token
            });
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        private string GenerateJwtToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GZGrzxj6a0G+hO2Fy6K3+n5UzO/GByYhPZ1T3vxA7Zs="));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Sub, "user"),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: "JobFindingIssuer",
                audience: "JobFindingAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(string toEmail)
        {
            var result = await service.SendToken(toEmail);
            if (result.Status != 200) return BadRequest(result);
            var smtpSettings = _configuration.GetSection("Smtp");

            var smtpClient = new SmtpClient(smtpSettings["Host"])
            {
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(smtpSettings["UserName"], smtpSettings["Password"]),
                EnableSsl = bool.Parse(smtpSettings["EnableSSL"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings["UserName"]),
                Subject = "Confirm Token",
                Body = (String)result.Data,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return Ok(result);
            }
            catch (SmtpException ex)
            {
                return StatusCode(500, $"Lỗi khi gửi email: {ex.Message}");
            }
        }
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(string email,string password,string token)
        {
            var result = await service.ResetPassword(email, password, token);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }

    }
}
