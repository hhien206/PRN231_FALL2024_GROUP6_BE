using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using BusinessObject.AddModel;
using Microsoft.AspNetCore.Http;
using Service.Service;
using BusinessObject.ViewModel;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("https://localhost:7008/");
        }

        [BindProperty]
        public AccountAdd Account { get; set; } = new AccountAdd();

        public string ErrorMessage { get; set; }
        public void OnGetAsync()
        {
            _httpContextAccessor.HttpContext.Session.Remove("AccountId");
            _httpContextAccessor.HttpContext.Session.Remove("Token");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var url = $"api/Account/LoginAccount?email={Account.email}&password={Account.password}";

            var loginData = new
            {
                email = Account.email,
                password = Account.password,
            };

            var response = await _httpClient.PostAsJsonAsync(url, loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult>();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                AccountView data = JsonSerializer.Deserialize<AccountView>(result.Data.ToString(), options);

                if (data != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetString("AccountId", data.AccountId.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("Token", data.Token);
                }

                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Login Fail!";
                return Page();
            }
        }
    }
}
