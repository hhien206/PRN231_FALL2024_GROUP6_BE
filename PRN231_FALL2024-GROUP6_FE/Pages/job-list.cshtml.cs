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

        public List<JobView> Jobs { get; set; } = new List<JobView>();

        public async Task OnGetAsync()
        {
            var result = await _jobService.ViewAllJob();
            if (result.Status == 200)
            {
                Jobs = (List<JobView>)result.Data;
            }
        }
    }
}
