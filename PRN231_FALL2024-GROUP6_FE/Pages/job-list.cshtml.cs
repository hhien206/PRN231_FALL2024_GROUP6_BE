using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;

namespace PRN231_FALL2024_GROUP6_FE.Pages
{
    public class job_listModel : PageModel
    {
        private readonly IJobService _jobService;

        public job_listModel(IJobService jobService)
        {
            _jobService = jobService;
        }

        public List<Job> Jobs { get; set; } = new List<Job>();

        public async Task OnGetAsync()
        {
            var result = await _jobService.ViewAllJob();
            if (result.Status == 200)
            {
                Jobs = (List<Job>)result.Data;
            }
        }
    }
}
