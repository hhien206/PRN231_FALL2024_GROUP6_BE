using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessObject.Models;

namespace PRN231_FALL2024_GROUP6_FE.Pages.JobLevels
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext _context;

        public DetailsModel(DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext context)
        {
            _context = context;
        }

        public JobLevel JobLevel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joblevel = await _context.JobLevels.FirstOrDefaultAsync(m => m.Id == id);
            if (joblevel == null)
            {
                return NotFound();
            }
            else
            {
                JobLevel = joblevel;
            }
            return Page();
        }
    }
}
