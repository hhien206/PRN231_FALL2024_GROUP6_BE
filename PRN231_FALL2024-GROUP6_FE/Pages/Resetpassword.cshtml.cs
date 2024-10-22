using BusinessObject.AddModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class ResetpasswordModel : PageModel
    {
        private HttpClient _httpClient;

        public ResetpasswordModel(HttpClient httpClient)
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

            // Tạo URL từ API của bạn
            string apiUrl = $"https://localhost:7008/api/Account/SendTokenReset?toEmail={Account.email}";

            try
            {
                // Gọi API để gửi token reset qua email
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    // Nếu thành công, điều hướng đến trang xác nhận
                    return RedirectToPage("ResetPasswordConfirmation");
                }
                else
                {
                    // Nếu thất bại, hiển thị thông báo lỗi
                    ModelState.AddModelError(string.Empty, "Không thể gửi yêu cầu reset mật khẩu.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi và hiển thị thông báo
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra: " + ex.Message);
                return Page();
            }
        }
    }
}

