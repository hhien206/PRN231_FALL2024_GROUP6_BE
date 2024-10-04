using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IJobCategoryRepository : IGenericRepository<JobCategory>
    {
        public Task<List<JobCategory>?> ListJobCategoryQuickSearch(string? nameQuickSerch);
    }
}
