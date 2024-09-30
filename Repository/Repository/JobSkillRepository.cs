using DataAccessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JobSkillRepository : GenericRepository<Jobkill>, IJobSkillRepository
    {
        public JobSkillRepository()
        {

        }
        public async Task<List<Jobkill>?> ListJobSkillQuickSearch(string? nameQuickSerch)
        {
            try
            {
                var listJobSkill = (await GetAllAsync()).ToList();
                if (nameQuickSerch == null) return listJobSkill;
                return listJobSkill?.FindAll(l => l.Name.ToLower().Contains(nameQuickSerch));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
