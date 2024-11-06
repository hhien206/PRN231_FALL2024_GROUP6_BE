using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service;
using System.Text;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages.HR.InterviewProcess
{
    public class EditInterviewModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public EditInterviewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public InterviewProcessUpdate interviewProocessUpdate { get; set; } = new InterviewProcessUpdate();

        public async Task<IActionResult> OnPostAsync(IFormFile imageUpload)
        {

            // Serialize the JobAdd object
            var jsonData = JsonSerializer.Serialize(interviewProocessUpdate);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // G?i yêu c?u POST ð? thêm Job
            var response = await _httpClient.PostAsync("https://localhost:7008/api/InterviewProcess/Update", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Error Editting.");
            return Page();
        }
        public void OnGet()
        {
            interviewProocessUpdate.InterviewProcessId = 1;
        }
    }
}
