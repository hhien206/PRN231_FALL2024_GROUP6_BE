using DataAccessObject.Models;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using Service.Service;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PRN231_FALL2024_GROUP6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "prn231-f86cb.appspot.com";
        private readonly IJobRepository _repo;

        public ImageUploadController(IJobRepository repo)
        {
            _storageClient = StorageClient.Create(Google.Apis.Auth.OAuth2.GoogleCredential.FromFile(@"C:\Users\PC\Downloads\TTTEST\prn231-f86cb-firebase-adminsdk-atx8n-1eaad06a10.json"));
            _repo = repo;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File không hợp lệ.");

            // Tạo tên file duy nhất
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                try
                {
                    // Upload lên Firebase Storage
                    await _storageClient.UploadObjectAsync(_bucketName, fileName, file.ContentType, stream);
                    var downloadUrl = $"https://storage.googleapis.com/{_bucketName}/{fileName}";

                    return Ok(new ServiceResult()
                    {
                        Status = 200,
                        Message = "Success",
                        Data = downloadUrl,
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new ServiceResult()
                    {
                        Status = 501,
                        Message = ex.Message
                    });
                }
            }
        }
    }
}
