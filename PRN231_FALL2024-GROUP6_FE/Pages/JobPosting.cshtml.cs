using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Service.Service;

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
        [BindProperty]
        public List<int> listJobId { get; set; } = new List<int>();
        public List<JobSkill> jobSkills { get; set; }

        public async Task<IActionResult> OnPostAsync(IFormFile imageUpload)
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            Job.AccountId = int.Parse(accountId);
            if (imageUpload != null && imageUpload.Length > 0)
            {
                // T?o MultipartFormDataContent ð? g?i ?nh
                var formData = new MultipartFormDataContent();
                using (var imageContent = new StreamContent(imageUpload.OpenReadStream()))
                {
                    imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(imageUpload.ContentType);
                    formData.Add(imageContent, "file", imageUpload.FileName);

                    // G?i yêu c?u POST ð?n API upload ?nh
                    var imageResponse = await _httpClient.PostAsync("https://localhost:7008/api/ImageUpload/upload", formData);

                    if (imageResponse.IsSuccessStatusCode)
                    {
                        // L?y k?t qu? response
                        var imageResult = await imageResponse.Content.ReadFromJsonAsync<ServiceResult>();
                        if (imageResult != null && imageResult.Status == 200)
                        {
                            // Gi? s? imageResult.Data ch?a URL ?nh
                            var imageUrl = imageResult.Data.ToString();
                            Job.UrlPicture = imageUrl; // Gán URL vào thu?c tính UrlImg c?a Job
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error uploading image.");
                        return Page();
                    }
                }
            }
            List<JobJobSkillAdd> jobSkills = new List<JobJobSkillAdd>();
            foreach (var id in listJobId)
            {
                jobSkills.Add(new JobJobSkillAdd
                {
                    JobId = id,
                    ExperienceRequirement = "1"
                });
            }
            Job.listJobSkill = jobSkills;
            Job.MaxQuantiy = 50;
            // Serialize the JobAdd object
            var jsonData = JsonSerializer.Serialize(Job);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // G?i yêu c?u POST ð? thêm Job
            var response = await _httpClient.PostAsync("https://localhost:7008/api/Job/Add", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Error adding job.");
            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnGetAsync()
        {

            var responseSkillAvailable = await _httpClient.GetAsync("https://localhost:7008/api/JobSkill/View");
            if (responseSkillAvailable.IsSuccessStatusCode)
            {
                var result = await responseSkillAvailable.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    jobSkills = JsonSerializer.Deserialize<List<JobSkill>>(result.Data.ToString(), options);
                }
            }
            else
            {
                Console.WriteLine($"Error: {responseSkillAvailable.StatusCode}");
            }
            return Page();
        }
    }
}
