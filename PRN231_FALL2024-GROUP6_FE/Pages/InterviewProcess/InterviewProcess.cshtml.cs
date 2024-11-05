using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages.InterviewProcess
{
    public class InterviewProcessModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public InterviewProcessModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<InterviewProcessView> interviews { get; set; } = new List<InterviewProcessView>();

        public async Task OnGetAsync()
        {
            var accountId = HttpContext.Session.GetString("AccountId");

            var response = await _httpClient.GetAsync($"https://localhost:7008/api/InterviewProcess/ViewUser?accountId={accountId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    interviews = JsonSerializer.Deserialize<List<InterviewProcessView>>(result.Data.ToString(), options);
                }
            }
        }
    }
}
