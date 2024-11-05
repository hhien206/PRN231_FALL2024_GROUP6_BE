using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service;
using System.Net.Http.Json;
using System.Text.Json;

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
        public CompanyView Company { get; set; } = new CompanyView();

        [BindProperty]
        public IFormFile CVFile { get; set; }

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
            var responseCompany = await _httpClient.GetAsync("https://localhost:7008/api/Company/View");
            if (responseCompany.IsSuccessStatusCode)
            {
                var result = await responseCompany.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    Company = JsonSerializer.Deserialize<CompanyView>(result.Data.ToString(), options);
                }
            }
        }

        public async Task<IActionResult> OnPostApplyAsync(int id)
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            if (accountId == null)
            {
                ModelState.AddModelError(string.Empty, "Vui lòng đăng nhập để nộp đơn.");
                return Page();
            }

            if (CVFile == null)
            {
                ModelState.AddModelError(string.Empty, "Vui lòng chọn một file CV.");
                return Page();
            }

            using var fileStream = CVFile.OpenReadStream();
            var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(fileStream), "file", CVFile.FileName);

            var uploadResponse = await _httpClient.PostAsync("https://localhost:7008/api/GoogleDrive/UploadFileCV", formData);

            if (!uploadResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Lỗi khi tải file lên Google Drive");
                TempData["ErrorMessage"] = "Lỗi khi tải file lên Google Drive.";
                return Page();
            }

            var uploadResult = await uploadResponse.Content.ReadFromJsonAsync<ServiceResult>();
            if (uploadResult == null || uploadResult.Status != 200)
            {
                Console.WriteLine("Tải file lên Google Drive không thành công");
                TempData["ErrorMessage"] = "Tải file lên Google Drive không thành công.";
                return Page();
            }

            var fileUrl = uploadResult.Data;

            var applicationData = new
            {
                accountId = accountId.ToString(),
                jobId = id,
                urlCV = fileUrl
            };

            var addApplicationResponse = await _httpClient.PostAsJsonAsync("https://localhost:7008/api/Application/Add", applicationData);

            if (!addApplicationResponse.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Lỗi khi nộp đơn, vui lòng thử lại sau.";
                return RedirectToPage();
            }

            TempData["SuccessMessage"] = "Đã nộp đơn thành công!";
            return RedirectToPage();
        }
    }
}
