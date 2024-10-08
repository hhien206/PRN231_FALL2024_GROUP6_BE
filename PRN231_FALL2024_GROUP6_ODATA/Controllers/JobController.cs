using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.IService;
using Service.Service;

namespace PRN231_FALL2024_GROUP6_ODATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ODataController
    {
        public IJobService service = new JobService();
        public JobController()
        {

        }
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await service.ViewAllJob();
                return Ok((List<JobView>)result.Data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{jobId}")]
        [EnableQuery]
        public async Task<IActionResult> Get(int jobId)
        {
            try
            {
                var result = await service.ViewJobDetail(jobId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok((JobView)result.Data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
