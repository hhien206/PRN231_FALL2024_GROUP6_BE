using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessObject.Models;

namespace PRN231_FALL2024_GROUP6_FE.Pages.JobCategories
{
    public class EditModel : PageModel
    {
        private readonly DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext _context;

        public EditModel(DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext context)
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

            var jobcategory =  await _context.JobCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (jobcategory == null)
            {
                return NotFound();
            }
            JobCategory = jobcategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(JobCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobCategoryExists(JobCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool JobCategoryExists(int id)
        {
            return _context.JobCategories.Any(e => e.Id == id);
        }
    }
}
