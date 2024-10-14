using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var json = JsonSerializer.Serialize(new { email = Email, password = Password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.GetAsync($"https://localhost:7008/api/Account/LoginAccount?email={Email}&password={Password}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseContent);
                ErrorMessage = jsonResponse["message"]?.ToString() ?? "Login failed!";
                return Page();
            }
        }

        public IActionResult OnGetRegister()
        {
            return RedirectToPage("/Register");  // Change to the path of your registration page
        }
    }
}
