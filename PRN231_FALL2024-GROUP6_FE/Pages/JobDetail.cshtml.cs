using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service;
using System.Net.Http.Json;

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
                return Page();
            }

            var uploadResult = await uploadResponse.Content.ReadFromJsonAsync<ServiceResult>();
            if (uploadResult == null || uploadResult.Status != 200)
            {
                Console.WriteLine("Tải file lên Google Drive không thành công");
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
                Console.WriteLine("Lỗi khi lưu ứng dụng vào cơ sở dữ liệu");
                return Page();
            }

            TempData["SuccessMessage"] = "Đã nộp đơn thành công!";
            return RedirectToPage();
        }
    }
}
