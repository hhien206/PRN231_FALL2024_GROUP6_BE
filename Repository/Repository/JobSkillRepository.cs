using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JobSkillRepository : GenericRepository<JobSkill>, IJobSkillRepository
    {
        public JobSkillRepository()
        {

        }
        public async Task<List<JobSkill>?> ListJobSkillQuickSearch(string? nameQuickSerch)
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
        public async Task<JobJobSkillView?> JobSkillViewDetail(int jobSkillId)
        {
            try
            {
                var jobSkill = GetById(jobSkillId);
                JobJobSkillView result = new();
                result.CovertJobSkillIntoJobJobSkillView(jobSkill);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
