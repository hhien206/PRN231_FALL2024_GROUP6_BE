using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessObject.Models;

namespace PRN231_FALL2024_GROUP6_FE.Pages.JobCategories
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext _context;

        public DeleteModel(DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public JobCategory JobCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobcategory = await _context.JobCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (jobcategory == null)
            {
                return NotFound();
            }
            else
            {
                JobCategory = jobcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobcategory = await _context.JobCategories.FindAsync(id);
            if (jobcategory != null)
            {
                JobCategory = jobcategory;
                _context.JobCategories.Remove(JobCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
