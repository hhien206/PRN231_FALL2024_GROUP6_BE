using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessObject.Models;

namespace PRN231_FALL2024_GROUP6_FE.Pages.JobTypes
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext _context;

        public DetailsModel(DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext context)
        {
            _context = context;
        }

        public JobType JobType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobtype = await _context.JobTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (jobtype == null)
            {
                return NotFound();
            }
            else
            {
                JobType = jobtype;
            }
            return Page();
        }
    }
}
