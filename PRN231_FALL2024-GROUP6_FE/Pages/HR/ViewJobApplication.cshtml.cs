using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN231_FALL2024_GROUP6_FE.Pages.Applicant;
using Service.Service;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages.HR
{
    public class ViewJobApplicationModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;
        private readonly HttpClient _httpClient;

        public ViewJobApplicationModel(ILogger<ProfileModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
        public List<JobView> Jobs { get; set; } = new List<JobView>();
        public List<ApplicationView> ApplicationViews { get; set; } = new List<ApplicationView>();
        public bool IsViewingApplications { get; set; } = false;


        public async Task<IActionResult> OnGetAsync()
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            var roleId = HttpContext.Session.GetInt32("Role");


            if (accountId == null || roleId != 2)
            {
                return RedirectToPage("/Index");
            }

            var response = await _httpClient.GetAsync($"https://localhost:7008/api/Job/ViewAllAccount?accountId={accountId}");
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
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostViewApplicationAsync(int jobId)
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            var roleId = HttpContext.Session.GetInt32("Role");

            if (accountId == null || roleId != 2)
            {
                return RedirectToPage("/Index");
            }

            var response = await _httpClient.GetAsync($"https://localhost:7008/api/Application/ViewJob?jobId={jobId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ApplicationViews = JsonSerializer.Deserialize<List<ApplicationView>>(result.Data.ToString(), options);
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
            IsViewingApplications = true;
            return Page();
        }
        public async Task<IActionResult> OnPostBackToJobListAsync()
        {
            IsViewingApplications = false; 
            var accountId = HttpContext.Session.GetString("AccountId");
            var response = await _httpClient.GetAsync($"https://localhost:7008/api/Job/ViewAllAccount?accountId={accountId}");

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
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostFilterJobAsync(int jobId)
        {
            var response = await _httpClient.PutAsync($"https://localhost:7008/api/Application/RefuseInsufficiant?jobId={jobId}", null);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "L?c th�nh c�ng.";
            }
            else
            {
                TempData["ErrorMessage"] = "L?c th?t b?i. Vui l?ng th? l?i.";
            }

            // T?i l?i danh s�ch c�ng vi?c sau khi l?c
            var accountId = HttpContext.Session.GetString("AccountId");
            var jobResponse = await _httpClient.GetAsync($"https://localhost:7008/api/Job/ViewAllAccount?accountId={accountId}");

            if (jobResponse.IsSuccessStatusCode)
            {
                var result = await jobResponse.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Jobs = JsonSerializer.Deserialize<List<JobView>>(result.Data.ToString(), options);
                }
            }
            else
            {
                Console.WriteLine($"Error: {jobResponse.StatusCode}");
            }

            return Page();
        }
    }
}
