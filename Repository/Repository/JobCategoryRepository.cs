using DataAccessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JobCategoryRepository : GenericRepository<JobCategory>, IJobCategoryRepository
    {
        public JobCategoryRepository()
        {

        }
        public async Task<List<JobCategory>?> ListJobCategoryQuickSearch(string? nameQuickSerch)
        {
            try
            {
                var listJobCategory = (await GetAllAsync()).ToList();
                if (nameQuickSerch == null) return listJobCategory;
                return listJobCategory?.FindAll(l => l.Name.ToLower().Contains(nameQuickSerch));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
