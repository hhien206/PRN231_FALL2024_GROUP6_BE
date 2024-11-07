using Azure;
using BusinessObject.UpdateModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
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
        [BindProperty]
        public InterviewProcessUpdate interviewProocessUpdate { get; set; } = new InterviewProcessUpdate();
        public InterviewProcessView interviewProocessView { get; set; } = new InterviewProcessView();

        public async Task<IActionResult> OnPostAsync()
        {
            var interviewId = RouteData.Values["id"]?.ToString();
            if (!ModelState.IsValid)
            {
                return Page(); // If form data is invalid, stay on the same page
            }

            // Serialize the InterviewProcessUpdate object
            interviewProocessUpdate.InterviewProcessId = int.Parse(interviewId);
            var jsonData = JsonSerializer.Serialize(interviewProocessUpdate);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Send POST request to update the interview process
            var response = await _httpClient.PostAsync("https://localhost:7008/api/InterviewProcess/Update", content);

            if (response.IsSuccessStatusCode)
            {
                // On success, redirect to a confirmation or list page
                return RedirectToPage("/HR/InterviewProcess/InterviewProcess");
            }

            // If failed, show error message
            ModelState.AddModelError(string.Empty, "Error editing the interview.");
            return Page();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get the interview ID from the URL
            var interviewId = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(interviewId))
            {
                return NotFound(); // If no ID is provided, return NotFound
            }

            var response = await _httpClient.GetAsync($"https://localhost:7008/api/InterviewProcess/ViewDetail?interviewProcessId={interviewId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    interviewProocessView = JsonSerializer.Deserialize<InterviewProcessView>(result.Data.ToString(), options);
                }
            }

            return Page();
        }
    }
}
