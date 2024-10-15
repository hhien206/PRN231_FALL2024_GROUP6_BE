using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class RegisterModel : PageModel
    {
      
            private readonly HttpClient _httpClient;

            public RegisterModel(IHttpClientFactory httpClientFactory)
            {
                _httpClient = httpClientFactory.CreateClient();
            }

            [BindProperty]
            public string Name { get; set; }

            [BindProperty]
            public string Username { get; set; }

            [BindProperty]
            public string Email { get; set; }

            [BindProperty]
            public string Password { get; set; }

            [BindProperty]
            public string RepeatPassword { get; set; }

            public string ErrorMessage { get; set; }
            public string SuccessMessage { get; set; }

            // Handle the form submission
            public async Task<IActionResult> OnPostAsync()
            {
                // Basic validation
                if (Password != RepeatPassword)
                {
                    ErrorMessage = "Passwords do not match.";
                    return Page();
                }

                var registrationData = new
                {
                    name = Name,
                    username = Username,
                    email = Email,
                    password = Password
                };

                var json = JsonSerializer.Serialize(registrationData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    // Send the data to the Register API endpoint
                    var response = await _httpClient.PostAsync("https://localhost:7008/api/Account/Register", content);

                    if (response.IsSuccessStatusCode)
                    {
                        SuccessMessage = "Registration successful! Please login.";
                        return RedirectToPage("/Login");  // Redirect to the login page after successful registration
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(responseContent);
                        ErrorMessage = jsonResponse["message"]?.ToString() ?? "Registration failed!";
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error: {ex.Message}";
                    return Page();
                }
            }
        }
    }
