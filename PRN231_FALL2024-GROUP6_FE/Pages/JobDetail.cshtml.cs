using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class JobDetailModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public JobDetailModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public JobView Job { get; set; } = new JobView();

        public async Task OnGetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7170/api/Job/{id}");
            if (response.IsSuccessStatusCode)
            {
                Job = await response.Content.ReadFromJsonAsync<JobView>();
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}
