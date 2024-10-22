/*using BusinessObject.AddModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class JobSkillsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public JobSkillsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public AccountAdd Account { get; set; } = new AccountAdd();


        public async Task OnGetAsync(int accountId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7008/api/AccountJobSkill/View?accountId={accountId}");
            if (response.IsSuccessStatusCode)
            {
                Account = await response.Content.ReadFromJsonAsync<AccountAdd>();
            }
        }
    }
        

}
*/