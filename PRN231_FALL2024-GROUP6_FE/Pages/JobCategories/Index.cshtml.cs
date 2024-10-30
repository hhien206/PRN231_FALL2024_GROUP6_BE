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
    public class IndexModel : PageModel
    {
        private readonly DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext _context;

        public IndexModel(DataAccessObject.Models.PRN231_DB_Job_RecruitmentContext context)
        {
            _context = context;
        }

        public IList<JobCategory> JobCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            JobCategory = await _context.JobCategories.ToListAsync();
        }
    }
}
