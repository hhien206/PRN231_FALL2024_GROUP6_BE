/*using BusinessObject.AddModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class ResetPasswordConfirmationModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ResetPasswordConfirmationModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public AccountAdd Account { get; set; } = new AccountAdd();

        public void OnGet(string token)
        {
            // Gán token từ URL vào Account.Token nếu cần thiết
            Account.Token = token;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Tạo JSON payload cho yêu cầu
            var jsonContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(Account),
                Encoding.UTF8,
                "application/json"
            );

            // Gửi yêu cầu tới API để đặt lại mật khẩu
            var response = await _httpClient.PostAsync($"https://localhost:7008/api/Account/ResetPassword", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Xử lý thành công, có thể chuyển hướng hoặc hiển thị thông báo
                return RedirectToPage("ResetPasswordSuccess"); // Thay bằng trang bạn muốn chuyển hướng
            }

            // Nếu không thành công, hiển thị lỗi
            ModelState.AddModelError(string.Empty, "Không thể đặt lại mật khẩu. Vui lòng thử lại.");
            return Page();
        }
    }
}
*/