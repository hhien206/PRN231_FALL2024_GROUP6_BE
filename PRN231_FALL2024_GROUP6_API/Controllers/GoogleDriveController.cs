using Google.Apis.Auth.OAuth2;
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
            try
            {
                string url = "https://drive.google.com/uc?export=download&id=1es37FCCWMv7IKzDiQWZT65yF24E2dDOr";
                string credentialPath = "Credentials/credentials.json";

                await DownloadFileAsync(url, credentialPath);

                string folderId = "1sxaRGZfe8isZemnf-aJ3Gd9kKU47AmBC";
                var urlLink = UploadFilesToGoogleDrive(credentialPath, folderId, file);

                if (System.IO.File.Exists(credentialPath))
                    System.IO.File.Delete(credentialPath);

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
        private async Task DownloadFileAsync(string fileId, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(fileId);
                response.EnsureSuccessStatusCode();

                using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
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
