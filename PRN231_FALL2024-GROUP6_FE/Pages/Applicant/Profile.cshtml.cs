using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages.Applicant
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;
        private readonly HttpClient _httpClient;

        public ProfileModel(ILogger<ProfileModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
        [BindProperty]
        public AccountView UserProfile { get; set; } = new AccountView();

        public AccountJobSkillAdd  jobSkillAdd { get; set; } = new AccountJobSkillAdd();

        public async Task<IActionResult> OnGetAsync()
        {
			var accountId = HttpContext.Session.GetString("AccountId");

			if (accountId == null)
            {
                return RedirectToPage("/Index");
            }

            var response = await _httpClient.GetAsync($"https://localhost:7008/api/Account/GetById?accountId={accountId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    UserProfile = JsonSerializer.Deserialize<AccountView>(result.Data.ToString(), options);
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var accountId = HttpContext.Session.GetString("AccountId");

            var jsonContent = JsonSerializer.Serialize(jobSkillAdd);
            Console.WriteLine(jsonContent);

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7257/api/NewsArticle/Add", jobSkillAdd);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Applicant/Profile");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Có l?i x?y ra: {errorMessage}");

            return Page();
        }
    }
}

