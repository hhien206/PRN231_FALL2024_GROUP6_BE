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
        [BindProperty]
        public InterviewProcessUpdate interviewProocessUpdate { get; set; } = new InterviewProcessUpdate();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // If form data is invalid, stay on the same page
            }

            // Serialize the InterviewProcessUpdate object
            var jsonData = JsonSerializer.Serialize(interviewProocessUpdate);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Send POST request to update the interview process
            var response = await _httpClient.PostAsync("https://localhost:7008/api/InterviewProcess/Update", content);

            if (response.IsSuccessStatusCode)
            {
                // On success, redirect to a confirmation or list page
                return RedirectToPage("/HR/InterviewProcess/Index");
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

            // Fetch the interview data from API
            var response = await _httpClient.GetAsync($"https://localhost:7008/api/InterviewProcess/ViewDetail?interviewProcessId={interviewId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                interviewProocessUpdate = JsonSerializer.Deserialize<InterviewProcessUpdate>(jsonResponse);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving interview details.");
                return Page();
            }

            return Page();
        }
    }
}
