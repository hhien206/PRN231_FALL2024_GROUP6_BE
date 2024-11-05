using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages.Applicant
{
    public class ApplicationModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ApplicationModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<ApplicationView> applications { get; set; } = new List<ApplicationView>();

        public async Task OnGetAsync()
        {
            var accountId = HttpContext.Session.GetString("AccountId");

            var response = await _httpClient.GetAsync($"https://localhost:7008/api/Application/ViewAccount?accountId={accountId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    applications = JsonSerializer.Deserialize<List<ApplicationView>>(result.Data.ToString(), options);
                }
            }
        }
    }
}
