
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        [HttpGet("Create")]
        public IActionResult CreatePdf()
        {
            string filePath = "encrypted_CV_NguyenVanA.pdf";

            // Gọi hàm tạo PDF với mã hóa và nhận đường dẫn file sau khi hoàn thành
            string pdfPath = CreateEncryptedPdf(filePath);
            return Ok(pdfPath);
        }
        public static string CreateEncryptedPdf(string dest)
        {
            // Mật khẩu để mã hóa file PDF
            string userPassword = "user123";
            string ownerPassword = "owner123";

            // Khởi tạo PDF writer với mã hóa AES 128
            PdfWriter writer = new PdfWriter(dest, new WriterProperties()
                .SetStandardEncryption(
                    System.Text.Encoding.UTF8.GetBytes(userPassword),    // Mật khẩu người dùng
                    System.Text.Encoding.UTF8.GetBytes(ownerPassword),   // Mật khẩu chủ sở hữu
                    EncryptionConstants.ALLOW_PRINTING,                  // Quyền
                    EncryptionConstants.ENCRYPTION_AES_128));            // Mã hóa AES 128

            // Tạo tài liệu PDF
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc);

            // Thêm nội dung vào file PDF
            document.Add(new Paragraph("Thông tin ứng viên mã hóa").SetBold().SetFontSize(20));
            document.Add(new Paragraph("Dữ liệu bí mật: Nguyễn Văn A, Kỹ sư phần mềm"));

            // Đóng tài liệu PDF sau khi hoàn tất
            document.Close();

            // Trả về đường dẫn của file PDF
            return dest;
        }
    }
}
