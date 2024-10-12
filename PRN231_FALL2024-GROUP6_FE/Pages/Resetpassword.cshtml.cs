using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class ResetpasswordModel : PageModel
    {
       
            private readonly HttpClient _httpClient;

            public ResetpasswordModel(IHttpClientFactory httpClientFactory)
            {
                _httpClient = httpClientFactory.CreateClient();
            }

            [BindProperty]
            public string Email { get; set; }

            public string Message { get; set; }
            public string ErrorMessage { get; set; }

            public async Task<IActionResult> OnPostAsync()
            {
                if (string.IsNullOrEmpty(Email))
                {
                    ErrorMessage = "Email is required.";
                    return Page();
                }

                var json = JsonSerializer.Serialize(new { email = Email });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await _httpClient.PostAsync("https://localhost:7008/api/Account/resetPassword", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Message = "Password reset link has been sent to your email.";
                        return Page();
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(responseContent);
                        ErrorMessage = jsonResponse["message"]?.ToString() ?? "Failed to send password reset link.";
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

