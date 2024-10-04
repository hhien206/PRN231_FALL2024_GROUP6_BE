using BusinessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JobLevelRepository : GenericRepository<JobLevel>, IJobLevelRepository
    {
        public JobLevelRepository()
        {

        }
        public async Task<List<JobLevel>?> ListJobLevelQuickSearch(string? nameQuickSerch)
        {
            try
            {
                var listJobLevel = (await GetAllAsync()).ToList();
                if (nameQuickSerch == null) return listJobLevel;
                return listJobLevel?.FindAll(l => l.Name.ToLower().Contains(nameQuickSerch));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
