using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IJobLevelRepository : IGenericRepository<JobLevel>
    {
        public Task<List<JobLevel>?> ListJobLevelQuickSearch(string? nameQuickSerch);
    }
}
