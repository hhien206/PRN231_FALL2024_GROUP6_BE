using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.AddModel; 

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class JobPostingModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public JobPostingModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public JobAdd Job { get; set; } = new JobAdd();

        public async Task<IActionResult> OnPostAsync()
        {
            // Serialize the JobAdd object
            var jsonData = JsonSerializer.Serialize(Job);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Send a POST request
            var response = await _httpClient.PostAsync("https://localhost:7008/api/Job/Add", content);

            // Check response status
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Error adding job.");
            return Page();
        }
    }
}
