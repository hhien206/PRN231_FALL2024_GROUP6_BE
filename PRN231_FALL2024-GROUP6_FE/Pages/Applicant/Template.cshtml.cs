using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages.Applicant
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;
        private readonly HttpClient _httpClient;

        public ProfileModel(ILogger<ProfileModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
        [BindProperty]
        public AccountView UserProfile { get; set; } = new AccountView();
        [BindProperty]
        public AccountJobSkillAdd  jobSkillAdd { get; set; } = new AccountJobSkillAdd();
        [BindProperty]
        public CertificateAdd certificateAdd { get; set; } = new CertificateAdd();
        [BindProperty]
        public EducateAdd educateAdd { get; set; } = new EducateAdd();
        [BindProperty]
        public List<JobSkill> jobSkillsAvailable { get; set; } = new List<JobSkill>();
        [BindProperty]
        public AccountUpdate accountUpdate { get; set; } = new AccountUpdate();

        public async Task<IActionResult> OnGetAsync()
        {
			var accountId = HttpContext.Session.GetString("AccountId");

			if (accountId == null)
            {
                return RedirectToPage("/Index");
            }

            var response = await _httpClient.GetAsync($"https://localhost:7008/api/Account/GetById?accountId={accountId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    UserProfile = JsonSerializer.Deserialize<AccountView>(result.Data.ToString(), options);
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

            var responseSkillAvailable = await _httpClient.GetAsync($"https://localhost:7008/api/AccountJobSkill/ViewSkillAvaliable?accountId={accountId}");
            if (responseSkillAvailable.IsSuccessStatusCode)
            {
                var result = await responseSkillAvailable.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    jobSkillsAvailable = JsonSerializer.Deserialize<List<JobSkill>>(result.Data.ToString(), options);
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var accountId = HttpContext.Session.GetString("AccountId");

            var jsonContent = JsonSerializer.Serialize(jobSkillAdd);
            Console.WriteLine(jsonContent);
            jobSkillAdd.AccountId = Convert.ToInt32(accountId);

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7008/api/AccountJobSkill/Add", jobSkillAdd);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Applicant/Template");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"C� l?i x?y ra: {errorMessage}");

            var updateData = new AccountUpdate
            {
                AccountId = UserProfile.AccountId,
                Description = accountUpdate.Description,

            };

            var responseUpdate = await _httpClient.PutAsJsonAsync("https://localhost:7008/api/Account/UpdateAccount", updateData);
            if (responseUpdate.IsSuccessStatusCode)
            {
                var result = await responseUpdate.Content.ReadFromJsonAsync<ServiceResult>();
                if (result != null && result.Status == 200)
                {
                    UserProfile.Description = accountUpdate.Description;
                    TempData["SuccessMessage"] = "Description updated successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error updating description.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Error updating description.";
            }

            // Redirect l?i trang �? hi?n th? k?t qu?
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteSkillAsync(int accountJobSkillId)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7008/api/AccountJobSkill/Delete?accountJobSkillId={accountJobSkillId}");
            if (response.IsSuccessStatusCode)
            {
                return new JsonResult(new { success = true });
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return new JsonResult(new { success = false, error = errorMessage });
        }
        public async Task<IActionResult> OnPostDeleteCertificateAsync(int accountCertificateId)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7008/api/Certificate/Delete?certificateId={accountCertificateId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Applicant/Template"); 
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Erorr: {errorMessage}");
            return Page();
        }
        public async Task<IActionResult> OnPostAddCertificateAsync()
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            certificateAdd.AccountId = Convert.ToInt32(accountId);
            var jsonData = JsonSerializer.Serialize(certificateAdd);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            
            var response = await _httpClient.PostAsync("https://localhost:7008/api/Certificate/Add", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Applicant/Template");
            }

            ModelState.AddModelError(string.Empty, "Error adding cer.");
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteEducationAsync(int educateId)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7008/api/Educate/Delete?educateId={educateId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Applicant/Template");
            }

            ModelState.AddModelError(string.Empty, "Error deleting education entry.");
            return Page();
        }
        public async Task<IActionResult> OnPostAddEducationAsync()
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            educateAdd.AccountId = Convert.ToInt32(accountId);
            var jsonData = JsonSerializer.Serialize(educateAdd);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7008/api/Educate/Add", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Applicant/Template");
            }

            ModelState.AddModelError(string.Empty, "Error adding education.");
            return Page();
        }
        public async Task<IActionResult> OnPostExportPdfAsync()
        {
            var accountId = HttpContext.Session.GetString("AccountId");

            if (accountId == null)
            {
                return RedirectToPage("/Index");
            }

            // G?i API l?y file PDF
            var response = await _httpClient.GetAsync($"https://localhost:7008/api/Pdf/generatepdf?accountId={accountId}");

            if (response.IsSuccessStatusCode)
            {
                var pdfContent = await response.Content.ReadAsByteArrayAsync();
                var fileName = $"Profile_{accountId}.pdf";

                // Tr? v? file �? t?i xu?ng
                return File(pdfContent, "application/pdf", fileName);
            }

            ModelState.AddModelError(string.Empty, "Failed to export PDF.");
            return Page();
        }
    }
}

