using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages.Applicant
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public ProfileModel(ILogger<ProfileModel> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public AccountView UserProfile { get; set; } 

        public async Task<IActionResult> OnGetAsync()
        {
			// L?y accountId t? session
			var accountId = HttpContext.Session.GetString("AccountId");

			if (accountId == null)
            {
                return RedirectToPage("/Index");
            }

            // G?i API ð? l?y thông tin ngý?i dùng theo accountId
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7008/api/Account/GetById?accountId={accountId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserProfile = JsonConvert.DeserializeObject<AccountView>(content);
            }
            else
            {
                _logger.LogError("Failed to load user profile");
            }

            return Page();
        }
    }
}

