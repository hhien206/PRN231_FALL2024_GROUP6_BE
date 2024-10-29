using DinkToPdf.Contracts;
using DinkToPdf;

using Microsoft.AspNetCore.Mvc;
using BusinessObject.ViewModel;
using Service.Service;
using Service.IService;
using DataAccessObject.Models;
using Rotativa.AspNetCore;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;

using iTextPath = iText.Kernel.Geom.Path;

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
                float percent = 0;
                if(skill.Experience != null)
                {
                    percent = (int)(float.Parse(skill.Experience) / 5f * 100);
                }
                skillsHtml += $"<li>\r\n             <div class=\"skill_name\">\r\n               {skill.JobSkillName}\r\n             </div>\r\n             <div class=\"skill_progress\">\r\n               <span style=\"width: {percent}%;\"></span>\r\n             </div>\r\n             <div class=\"skill_per\">{percent}%</div>\r\n           </li>";
            }
            var certificatesHtml = "";
            foreach (var certificate in account.Certificates)
            {
                certificatesHtml += $"<li>                <div class=\"date\">{certificate.CertificateName}</div>                 <div class=\"info\">                     <p class=\"semi-bold\">Coursera</p>                   <p>{certificate.CertificateUrl}</p>                </div>            </li>";
            }
            var educatesHtml = "";
            foreach(var educate in account.Educates)
            {
                
                educatesHtml += $"<li>                <div class=\"date\">{educate.Date}</div>                 <div class=\"info\">                     <p class=\"semi-bold\">{educate.EducateName}</p>                   <p>{educate.Description}</p>                </div>            </li>";
            }
            var htmlContent = LayoutPdf(account.UrlPicture,account.FullName,account.Gender,account.Email,account.PhoneNumber,account.Address,account.Description,skillsHtml,certificatesHtml,educatesHtml);

            var pdfFileName = "output.pdf";
            var pdfPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), pdfFileName);

            using (var fileStream = new FileStream(pdfPath, FileMode.Create, FileAccess.Write))
            {
                // Đặt kích thước trang PDF
                var pageSize = iText.Kernel.Geom.PageSize.A3;

                // Tạo PdfWriter với nén
                var pdfWriter = new iText.Kernel.Pdf.PdfWriter(fileStream, new iText.Kernel.Pdf.WriterProperties().SetCompressionLevel(9));
                var pdfDocument = new iText.Kernel.Pdf.PdfDocument(pdfWriter);

                // Đặt kích thước trang cho tài liệu PDF
                pdfDocument.SetDefaultPageSize(pageSize);

                // Thay đổi kích thước PDF
                var converterProperties = new ConverterProperties();
                converterProperties.SetBaseUri("baseUri"); // Nếu có sử dụng CSS từ file
                                                           // Chuyển đổi HTML thành PDF
                HtmlConverter.ConvertToPdf(htmlContent, pdfDocument, converterProperties);

                pdfDocument.Close(); // Đảm bảo đóng tài liệu PDF
            }

            return PhysicalFile(pdfPath, "application/pdf", pdfFileName);
        }
        private string LayoutPdf(string imageUrl,string fullName, string gender, string email, string phoneNumber, string address, string description,string skill, string certificate, string educate)
        {
            var htmlContent = $@"<script src=""https://kit.fontawesome.com/b99e675b6e.js""></script>

<style>
  @import url(""https://fonts.googleapis.com/css?family=Montserrat:400,500,700&display=swap"");

* {{
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  list-style: none;
  font-family: ""Montserrat"", sans-serif;
}}

body {{
  background: #585c68;
  font-size: 14px;
  line-height: 22px;
  color: #555555;
}}

.bold {{
  font-weight: 700;
  font-size: 20px;
  text-transform: uppercase;
}}

.semi-bold {{
  font-weight: 500;
  font-size: 16px;
}}

.resume {{
  width: 800px;
  height: auto;
  display: flex;
  margin: 50px auto;
}}

.resume .resume_left {{
  width: 280px;
  background: #0bb5f4;
}}

.resume .resume_left .resume_profile {{
  width: 100%;
  height: 280px;
}}

.resume .resume_left .resume_profile img {{
  width: 100%;
  height: 100%;
}}

.resume .resume_left .resume_content {{
  padding: 0 25px;
}}

.resume .title {{
  margin-bottom: 20px;
}}

.resume .resume_left .bold {{
  color: #fff;
}}

.resume .resume_left .regular {{
  color: #b1eaff;
}}

.resume .resume_item {{
  padding: 25px 0;
  border-bottom: 2px solid #b1eaff;
}}

.resume .resume_left .resume_item:last-child,
.resume .resume_right .resume_item:last-child {{
  border-bottom: 0px;
}}

.resume .resume_left ul li {{
  display: flex;
  margin-bottom: 10px;
  align-items: center;
}}

.resume .resume_left ul li:last-child {{
  margin-bottom: 0;
}}

.resume .resume_left ul li .icon {{
  width: 35px;
  height: 35px;
  background: #fff;
  color: #0bb5f4;
  border-radius: 50%;
  margin-right: 15px;
  font-size: 16px;
  position: relative;
}}

.resume .icon i,
.resume .resume_right .resume_hobby ul li i {{
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}}

.resume .resume_left ul li .data {{
  color: #b1eaff;
}}

.resume .resume_left .resume_skills ul li {{
  display: flex;
  margin-bottom: 10px;
  color: #b1eaff;
  justify-content: space-between;
  align-items: center;
}}

.resume .resume_left .resume_skills ul li .skill_name {{
  width: 25%;
}}

.resume .resume_left .resume_skills ul li .skill_progress {{
  width: 60%;
  margin: 0 5px;
  height: 5px;
  background: #009fd9;
  position: relative;
}}

.resume .resume_left .resume_skills ul li .skill_per {{
  width: 15%;
}}

.resume .resume_left .resume_skills ul li .skill_progress span {{
  position: absolute;
  top: 0;
  left: 0;
  height: 100%;
  background: #fff;
}}

.resume .resume_left .resume_social .semi-bold {{
  color: #fff;
  margin-bottom: 3px;
}}

.resume .resume_right {{
  width: 520px;
  background: #fff;
  padding: 25px;
}}

.resume .resume_right .bold {{
  color: #0bb5f4;
}}

.resume .resume_right .resume_work ul,
.resume .resume_right .resume_education ul {{
  padding-left: 40px;
  overflow: hidden;
}}

.resume .resume_right ul li {{
  position: relative;
}}

.resume .resume_right ul li .date {{
  font-size: 16px;
  font-weight: 500;
  margin-bottom: 15px;
}}

.resume .resume_right ul li .info {{
  margin-bottom: 20px;
}}

.resume .resume_right ul li:last-child .info {{
  margin-bottom: 0;
}}

.resume .resume_right .resume_work ul li:before,
.resume .resume_right .resume_education ul li:before {{
  content: """";
  position: absolute;
  top: 5px;
  left: -25px;
  width: 6px;
  height: 6px;
  border-radius: 50%;
  border: 2px solid #0bb5f4;
}}

.resume .resume_right .resume_work ul li:after,
.resume .resume_right .resume_education ul li:after {{
  content: """";
  position: absolute;
  top: 14px;
  left: -21px;
  width: 2px;
  height: 115px;
  background: #0bb5f4;
}}

.resume .resume_right .resume_hobby ul {{
  display: flex;
  justify-content: space-between;
}}

.resume .resume_right .resume_hobby ul li {{
  width: 80px;
  height: 80px;
  border: 2px solid #0bb5f4;
  border-radius: 50%;
  position: relative;
  color: #0bb5f4;
}}

.resume .resume_right .resume_hobby ul li i {{
  font-size: 30px;
}}

.resume .resume_right .resume_hobby ul li:before {{
  content: """";
  position: absolute;
  top: 40px;
  right: -52px;
  width: 50px;
  height: 2px;
  background: #0bb5f4;
}}

.resume .resume_right .resume_hobby ul li:last-child:before {{
  display: none;
}}
</style>


<div class=""resume"">
   <div class=""resume_left"">
     <div class=""resume_profile"">
       <img src=""{imageUrl}"" alt=""profile_pic"">
     </div>
     <div class=""resume_content"">
       <div class=""resume_item resume_info"">
         <div class=""title"">
           <p class=""bold""> {fullName}</p>
           <p class=""regular"">{gender}</p>
         </div>
         <ul>
           <li>
             <div class=""icon"">
               <i class=""fas fa-map-signs""></i>
             </div>
             <div class=""data"">
              {address} <br />
             </div>
           </li>
           <li>
             <div class=""icon"">
               <i class=""fas fa-mobile-alt""></i>
             </div>
             <div class=""data"">
              {phoneNumber}
             </div>
           </li>
           <li>
             <div class=""icon"">
               <i class=""fas fa-envelope""></i>
             </div>
             <div class=""data"">
              {email}
             </div>
           </li>
         </ul>
       </div>
       <div class=""resume_item resume_skills"">
         <div class=""title"">
           <p class=""bold"">skill's</p>
         </div>
         <ul>
          {skill}
         </ul>
       </div>
       <div class=""resume_item resume_social"">
         <div class=""title"">
           <p class=""bold"">Social</p>
         </div>
         <ul>
           <li>
             <div class=""icon"">
               <i class=""fab fa-facebook-square""></i>
             </div>
             <div class=""data"">
               <p class=""semi-bold"">Facebook</p>
               <p>Stephen@facebook</p>
             </div>
           </li>
           <li>
             <div class=""icon"">
               <i class=""fab fa-twitter-square""></i>
             </div>
             <div class=""data"">
               <p class=""semi-bold"">Twitter</p>
               <p>Stephen@twitter</p>
             </div>
           </li>
           <li>
             <div class=""icon"">
               <i class=""fab fa-youtube""></i>
             </div>
             <div class=""data"">
               <p class=""semi-bold"">Youtube</p>
               <p>Stephen@youtube</p>
             </div>
           </li>
           <li>
             <div class=""icon"">
               <i class=""fab fa-linkedin""></i>
             </div>
             <div class=""data"">
               <p class=""semi-bold"">Linkedin</p>
               <p>Stephen@linkedin</p>
             </div>
           </li>
         </ul>
       </div>
     </div>
  </div>
  <div class=""resume_right"">
    <div class=""resume_item resume_about"">
        <div class=""title"">
           <p class=""bold"">About us</p>
         </div>
        <p>{description}</p>
    </div>
    <div class=""resume_item resume_work"">
        <div class=""title"">
           <p class=""bold"">Work Experience</p>
         </div>
        <ul>
          {certificate}
        </ul>
    </div>
    <div class=""resume_item resume_education"">
      <div class=""title"">
           <p class=""bold"">Education</p>
         </div>
      <ul>
        {educate}
        </ul>
    </div>
</div>";
            return htmlContent;
        }
    }
}
