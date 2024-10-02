using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IJobTypeRepository : IGenericRepository<JobType>
    {
        public Task<List<JobType>?> ListJobTypeQuickSearch(string? nameQuickSerch);
    }
}
