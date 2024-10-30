using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Newtonsoft.Json;
using BusinessObject.AddModel;

namespace PRN231_FALL2024_GROUP6_FE.Pages.Authentication
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public RegisterModel(HttpClient httpClient)
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


                // Convert the register data to JSON format
                var jsonContent = new StringContent(JsonConvert.SerializeObject(Account), Encoding.UTF8, "application/json");

                // Send the POST request to your register API
                var response = await _httpClient.PostAsync("https://localhost:7008/api/Account/Register", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    // Handle successful registration (e.g., redirect to login)
                    return RedirectToPage("/Authentication/Login");
                }
                else
                {
                    // Handle registration failure
                    ViewData["ErrorMessage"] = "Registration failed. Please try again.";
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
