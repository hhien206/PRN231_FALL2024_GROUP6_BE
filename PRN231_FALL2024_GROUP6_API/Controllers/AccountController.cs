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
using BusinessObject.ViewModel.Request;
using BusinessObject.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;

namespace PRN231_FALL2024_GROUP6_API.Controllers
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
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (User.Identity.IsAuthenticated)
                return BadRequest("Already logged in");

            var result = await service.LoginAccount(request);
            if (result.Status == 200)
            {
                var token = GenerateJwtToken((Account)result.Data, request.RememberMe);
                return Ok(new { message = "Login successful", token});
            }
            return BadRequest("Invalid email or password");
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            if (User.Identity.IsAuthenticated)
                return BadRequest("Already logged in");

            if (request.Password != request.ConfirmPassword)
                return BadRequest("Password and Confirm Password must match");

            var result = await service.AddAccount(request);

            if (result.Status == 200)
                return Ok("Signup successful");

            return BadRequest("Signup failed");
        }

        private string GenerateJwtToken(Account user, bool rememberMe)
        {
            var claims = new[]
            {        
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ((int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()).ToString()),
                new Claim(ClaimTypes.Sid, user.AccountId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId?.ToString()!),
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            DateTime expiration = rememberMe
                ? DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpiresInRememberMe"]!))
                : DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpiresIn"]!));


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost("addAccount")]
        public async Task<IActionResult> AddAccount([FromBody] CreateAccountRequest request)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await service.AddAccountAsync(request);

                if (result.Status == 200)
                    return Ok(new { message = "Account added successfully", data = result.Data });

                return BadRequest(result.Message);
            }
            return Unauthorized("You are not authorized to perform this action.");
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var result = await service.GetAllAccountsAsync();
            if (result.Status == 200)
                return Ok(result.Data);

            return StatusCode(result.Status, result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var result = await service.GetAccountByIdAsync(id);
            if (result.Status == 200)
                return Ok(result.Data);

            return NotFound(result.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
        {
            var result = await service.UpdateAccountAsync(request);
            if (result.Status == 200)
                return Ok(result.Data);

            return StatusCode(result.Status, result.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = await service.DeleteAccountAsync(id);
            if (result.Status == 200)
                return Ok("Account deleted successfully.");

            return StatusCode(result.Status, result.Message);
        }
        //[HttpGet("ViewList")]
        //[EnableQuery]
        //public async Task<IActionResult> ViewListAccount(int sizePaging,int indexPaging)
        //{
        //    var result = await service.ViewListAccount(sizePaging, indexPaging);
        //    if (result.Status == 200) return Ok(result);
        //    else return BadRequest(result);
        //}
        //[HttpGet("Login")]
        //public async Task<IActionResult> LoginAccount(string email, string password)
        //{
        //    var result = await service.LoginAccount(email, password);
        //    if (result.Status == 200) return Ok(result);
        //    else return BadRequest(result);
        //}
        ///*[HttpGet("LoginToken")]
        //public async Task<IActionResult> LoginToken(string token)
        //{
        //    var result = await service.LoginToken(token);
        //    if (result.Status == 200) return Ok(result);
        //    else return BadRequest(result);
        //}*/
        //[HttpGet("Register")]
        //public async Task<IActionResult> Register(string email,string password)
        //{
        //    var token = GenerateJwtToken(email);
        //    var result = await service.AddAccount(new AccountAdd
        //    {
        //        userName = email,
        //        password = password,
        //        accessToken = token
        //    });
        //    if (result.Status == 200) return Ok(result);
        //    else return BadRequest(result);
        //}
        //private string GenerateJwtToken(string email)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GZGrzxj6a0G+hO2Fy6K3+n5UzO/GByYhPZ1T3vxA7Zs="));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Email, email),
        //        new Claim(JwtRegisteredClaimNames.Sub, "user"),
        //        new Claim(ClaimTypes.Role, "User"),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };
        //    var token = new JwtSecurityToken(
        //        issuer: "JobFindingIssuer",
        //        audience: "JobFindingAudience",
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(30),
        //        signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        //[HttpPost("send")]
        //public async Task<IActionResult> SendEmail(string toEmail)
        //{
        //    var result = await service.SendToken(toEmail);
        //    if (result.Status != 200) return BadRequest(result);
        //    var smtpSettings = _configuration.GetSection("Smtp");

        //    var smtpClient = new SmtpClient(smtpSettings["Host"])
        //    {
        //        Port = int.Parse(smtpSettings["Port"]),
        //        Credentials = new NetworkCredential(smtpSettings["UserName"], smtpSettings["Password"]),
        //        EnableSsl = bool.Parse(smtpSettings["EnableSSL"])
        //    };

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(smtpSettings["UserName"]),
        //        Subject = "Confirm Token",
        //        Body = (String)result.Data,
        //        IsBodyHtml = true,
        //    };

        //    mailMessage.To.Add(toEmail);

        //    try
        //    {
        //        await smtpClient.SendMailAsync(mailMessage);
        //        return Ok(result);
        //    }
        //    catch (SmtpException ex)
        //    {
        //        return StatusCode(500, $"Lỗi khi gửi email: {ex.Message}");
        //    }
        //}
        //[HttpPost("resetPassword")]
        //public async Task<IActionResult> ResetPassword(string email,string password,string token)
        //{
        //    var result = await service.ResetPassword(email, password, token);
        //    if (result.Status == 200) return Ok(result);
        //    else return BadRequest(result);
        //}

    }
    }
