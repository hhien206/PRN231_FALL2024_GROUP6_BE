using BusinessObject.AddModel;
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
        public List<AccountView> HRAccounts { get; set; }

        [BindProperty]
        public InterviewProcessAdd InterviewProcessAdd { get; set; }
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

            response = await _httpClient.GetAsync($"https://localhost:7008/api/Account/GetAllHR");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    HRAccounts = JsonSerializer.Deserialize<List<AccountView>>(result.Data.ToString(), options);
                }
            }

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
                TempData["SuccessMessage"] = "L?c thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "L?c th?t b?i. Vui l?ng th? l?i.";
            }

            // T?i l?i danh sách công vi?c sau khi l?c
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
        public async Task<IActionResult> OnPostAcceptApplicationAsync()
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            var roleId = HttpContext.Session.GetInt32("Role");

            if (accountId == null || roleId != 2)
            {
                return RedirectToPage("/Index");
            }

            var response = await _httpClient.PutAsync($"https://localhost:7008/api/Application/Accept?applicationId={InterviewProcessAdd.ApplicationId}", null);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Accept Application Success";
            }
            else
            {
                TempData["ErrorMessage"] = "Accpet Application Fail. Try Again!";
            }

            var jsonContent = JsonSerializer.Serialize(InterviewProcessAdd);
            response = await _httpClient.PostAsJsonAsync("https://localhost:7008/api/InterviewProcess/Add", InterviewProcessAdd);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/HR/ViewJobApplication");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Có l?i x?y ra: {errorMessage}");

            // T?i l?i danh sách ?ng tuy?n sau khi th?c hi?n hành ð?ng
            await LoadApplicationsAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostDenyApplicationAsync(int applicationId)
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            var roleId = HttpContext.Session.GetInt32("Role");

            if (accountId == null || roleId != 2)
            {
                return RedirectToPage("/Index");
            }

            // S? d?ng HttpPut ð? g?i API t? ch?i (Refuse)
            var response = await _httpClient.PutAsync($"https://localhost:7008/api/Application/Refuse?applicationId={applicationId}", null);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "T? ch?i ?ng tuy?n thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "T? ch?i ?ng tuy?n không thành công. Vui l?ng th? l?i.";
            }

            // T?i l?i danh sách ?ng tuy?n sau khi th?c hi?n hành ð?ng
            await LoadApplicationsAsync();

            return Page();
        }

        private async Task LoadApplicationsAsync()
        {
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
        }
    }
}
