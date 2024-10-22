using DinkToPdf.Contracts;
using DinkToPdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IConverter _converter;

        public PdfController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet("generatepdf")]
        public IActionResult GeneratePdf()
        {
            // Nội dung HTML
            var htmlContent = @"
                <html>
                    <head>
                        <style>
                            body { font-family: 'DejaVu Sans', sans-serif; }
                            h1 { text-align: center; }
                            .info { text-align: center; margin-bottom: 20px; }
                            .section { margin-top: 20px; }
                            .title { font-weight: bold; font-size: 14pt; }
                            .text { font-size: 12pt; }
                            .line { border-bottom: 1px solid black; margin-top: 10px; }
                        </style>
                    </head>
                    <body>
                        <h1>Hieu</h1>
                        <div class='info'>
                            <p>Male</p>
                            <p>nguyenvana@gmail.com | 0123456789 | Hà Nội</p>
                        </div>
                        <div class='line'></div>
                        <div class='section'>
                            <p class='title'>JOB SKILL</p>
                            <ul class='text'>
                                <li>Trường timviec.com.vn (03/2015 - Hiện tại) - Giáo viên: Đảm bảo chất lượng chăm sóc và giáo dục trẻ theo tiêu chí của trường.</li>
                                <li>Trung tâm timviec.com.vn (11/2014 - 02/2015) - Trợ giảng.</li>
                                <li>Trung tâm gia sư timviec.com.vn (06/2014 - 01/2014) - Gia sư.</li>
                            </ul>
                        </div>
                        <div class='section'>
                            <p class='title'>CERTIFICATES</p>
                            <ul class='text'>
                                <li>Trường timviec.com.vn (03/2015 - Hiện tại) - Giáo viên: Đảm bảo chất lượng chăm sóc và giáo dục trẻ theo tiêu chí của trường.</li>
                                <li>Trung tâm timviec.com.vn (11/2014 - 02/2015) - Trợ giảng.</li>
                                <li>Trung tâm gia sư timviec.com.vn (06/2014 - 01/2014) - Gia sư.</li>
                            </ul>
                        </div>
                        <div class='section'>
                            <p class='title'>HOẠT ĐỘNG</p>
                            <p class='text'>Nhóm tình nguyện timviec.com.vn - Tình nguyện viên (10/2013 - 08/2014)</p>
                        </div>
                    </body>
                </html>";

            // Cấu hình chuyển đổi HTML sang PDF
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            // Chuyển đổi HTML sang PDF
            byte[] pdf = _converter.Convert(doc);

            // Trả về file PDF
            return File(pdf, "application/pdf", "cv_nguyenvana.pdf");
        }
    }
}
