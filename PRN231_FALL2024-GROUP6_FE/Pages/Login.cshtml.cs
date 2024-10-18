using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using BusinessObject.AddModel;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public AccountAdd Account { get; set; } = new AccountAdd();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Create the API URL with query parameters for email and password
                var requestUri = $"https://localhost:7008/api/Account/LoginAccount?email={Account.email}&password={Account.password}";

                // Send the GET request to your login API
                var response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    // Optionally, you can read the response content (e.g., JWT token or user data)
                    var responseData = await response.Content.ReadAsStringAsync();
                    // Process the response (e.g., save token, redirect)
                    return RedirectToPage("/Index"); // Redirect to a dashboard after successful login
                }
                else
                {
                    // Handle invalid login response
                    ViewData["ErrorMessage"] = "Invalid login credentials.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
    }
}
