using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessObject.Models;
using System.Security.Claims;
using System;
using BusinessObject.ViewModel;
using Service.Service;
using Service.IService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.OData.Query;
using BusinessObject.AddModel;
using Microsoft.AspNetCore.Authorization;
using BusinessObject.UpdateModel;

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
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int sizePaging, int indexPaging)
        {
            var result = await service.GetAllAccount(sizePaging, indexPaging);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int accountId)
        {
            var result = await service.GetAccountById(accountId);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost("LoginAccount")]
        public async Task<IActionResult> LoginAccount(string email, string password)
        {
            var result = await service.LoginAccount(email, password);
            
            if (result.Status == 200)
            {
                string role = ((AccountView)result.Data).Role.RoleName;
                var token = GenerateJwtToken(email, role);
                var account = (AccountView)result.Data;
                account.Token = token;
                return Ok(result);
            }
            else return BadRequest(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AccountAdd key)
        {
            var result = await service.AddAccount(key, 3);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(AccountAdd key)
        {
            var result = await service.AddAccount(key, 1);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPut("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount(AccountUpdate key)
        {
            var result = await service.UpdateAccount(key);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GZGrzxj6a0G+hO2Fy6K3+n5UzO/GByYhPZ1T3vxA7Zs="));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, "testuser"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            var token = new JwtSecurityToken(
                issuer: "IndieGameIssuer",
                audience: "IndieGameAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost("SendTokenReset")]
        public async Task<IActionResult> SendTokenReset(string toEmail)
        {
            try
            {
                var result = await service.SendToken(toEmail, "FORGET");
                if (result.Status != 200) return BadRequest(result);
                await SendMail(toEmail, "Mã reset password của bạn là: " + (string)result.Data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi gửi email: {ex.Message}");
            }
        }
        [HttpPost("SendTokenVerify")]
        public async Task<IActionResult> SendTokenVerify(string toEmail)
        {
            try
            {
                var result = await service.SendToken(toEmail, "VERIFY");
                if (result.Status != 200) return BadRequest(result);
                await SendMail(toEmail, "mã xác nhận tài khoản của bạn là: " + (string)result.Data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi gửi email: {ex.Message}");
            }
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string password, string token)
        {
            var result = await service.ResetPassword(email, password, token);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost("VerifyAccount")]
        public async Task<IActionResult> VerifyAccount(string email, string token)
        {
            var result = await service.VerifyAccount(email, token);
            if (result.Status == 200) return Ok(result);
            else return BadRequest(result);
        }
        private async Task<IActionResult> SendMail(string toEmail, string message)
        {
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
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return Ok();
            }
            catch (SmtpException ex)
            {
                throw;
            }
        }
        private string GenerateJwtToken(string email, string role)
        {
            var key = _configuration["JwtSettings:Key"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
