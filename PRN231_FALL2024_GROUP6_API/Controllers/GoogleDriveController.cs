﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.Service;

namespace IndieGameHubSever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleDriveController : ControllerBase
    {
        [HttpPost("UploadFileCV")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                string credentialPath = "Credentials/credentials.json";
                string folderId = "1sxaRGZfe8isZemnf-aJ3Gd9kKU47AmBC";
                var urlLink = UploadFilesToGoogleDrive(credentialPath, folderId, file);

                return Ok(new ServiceResult
                {
                    Status = 200,
                    Message = "Success",
                    Data = urlLink
                });
            }
            catch (Exception ex)
            {
                return Ok(new ServiceResult
                {
                    Status = 501,
                    Message = "Error",
                    Data = ex.Message
                });
            }
            
        }
        private string UploadFilesToGoogleDrive(string credentialPath,string folderId, IFormFile fileToUpload)
        {
            GoogleCredential credential;
            using(var stream =new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(new[]
                {
                    DriveService.ScopeConstants.DriveFile
                });
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "IndieHubGame"
                });

                var fileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(fileToUpload.FileName),
                    Parents = new List<string> { folderId }
                };
                FilesResource.CreateMediaUpload request;
                using(var stream1 = fileToUpload.OpenReadStream())
                {
                    request = service.Files.Create(fileMetaData, stream1, "");
                    request.Fields = "id";
                    request.Upload();
                }
                var uploadedFile = request.ResponseBody;
                var permission = new Google.Apis.Drive.v3.Data.Permission()
                {
                    Type = "anyone",
                    Role = "reader"
                };
                service.Permissions.Create(permission, uploadedFile.Id).Execute();
                var sharedFile = service.Files.Get(uploadedFile.Id).Execute();
                return "https://drive.google.com/file/d/" + sharedFile.Id + "/view";
            }
        }
    }
}
