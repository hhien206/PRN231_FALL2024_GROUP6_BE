using DinkToPdf.Contracts;
using DinkToPdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using BusinessObject.ViewModel;
using Service.Service;
using Service.IService;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IConverter _converter;
        private IAccountService service = new AccountService();

        public PdfController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet("generatepdf")]
        public async Task<IActionResult> GeneratePdf(int accountId)
        {
            var result = await service.GetAccountById(accountId);
            AccountView account = (AccountView)result.Data;
            var skillsHtml = "";
            foreach (var skill in account.AccountJobSkill)
            {
                skillsHtml += $"<li>Skill: {skill.JobSkillName}({skill.Experience})</li>";
            }
            var certificatesHtml = "";
            foreach (var certificate in account.Certificates)
            {
                certificatesHtml += $"<li>Certificate Name: {certificate.CertificateName}({certificate.CertificateUrl})</li>";
            }
            var htmlContent = $@"
             <html>
                <head>
                    <style>
                        body {{{{ font-family: 'DejaVu Sans', sans-serif; }}}}
                        h1 {{{{ text-align: center; }}}}
                        .info {{{{ text-align: center; margin-bottom: 20px; }}}}
                        .section {{{{ margin-top: 20px; }}}}
                    .title {{{{ font-weight: bold; font-size: 14pt; }}}}
                    .text {{{{ font-size: 12pt; }}}}
                    .line {{{{ border-bottom: 1px solid black; margin-top: 10px; }}}}
                    </style>
            </head>
            <body>
            <h1>{account.FullName}</h1>
            <div class='info'>
                <p>{account.Gender}</p>
                <p>{account.Email} | {account.PhoneNumber} | {account.Address}</p>
            </div>
            <div class='line'></div>
            <div class='section'>
                <p class='title'>JOB SKILL</p>
                <ul class='text'>
                    {skillsHtml}
                </ul>
            </div>
            <div class='section'>
                <p class='title'>CERTIFICATES</p>
                <ul class='text'>
                    {certificatesHtml}
                </ul>
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
