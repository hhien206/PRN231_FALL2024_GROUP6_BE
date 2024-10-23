using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [BindProperty]
        public List<JobView> Jobs { get; set; } = new List<JobView>();

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7008/api/Job/ViewAll");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    Jobs = JsonSerializer.Deserialize<List<JobView>>(result.Data.ToString(), options);
                }
            }
        }
    }
}