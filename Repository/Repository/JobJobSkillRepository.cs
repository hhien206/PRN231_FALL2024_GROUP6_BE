using DataAccessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JobJobSkillRepository : GenericRepository<JobJobSkill>, IJobJobSkillRepository
    {
        public JobJobSkillRepository()
        {

        }
        public async Task<List<JobJobSkill>> GetAllJobJobSkillByJobId(int jobId)
        {
            return (await GetAllAsync()).FindAll(l => l.JobId == jobId);
        }
    }
}
