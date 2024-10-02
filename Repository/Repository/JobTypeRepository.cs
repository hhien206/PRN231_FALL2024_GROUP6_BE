using DataAccessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JobTypeRepository : GenericRepository<JobType>, IJobTypeRepository
    {
        public JobTypeRepository()
        {

        }
        public async Task<List<JobType>?> ListJobTypeQuickSearch(string? nameQuickSerch)
        {
            try
            {
                var listJobType = (await GetAllAsync()).ToList();
                if (nameQuickSerch == null) return listJobType;
                return listJobType?.FindAll(l => l.Name.ToLower().Contains(nameQuickSerch));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
