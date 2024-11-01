using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAccountJobSkillRepository:IGenericRepository<AccountJobSkill>
    {
        public Task<List<AccountJobSkillView>> ListAccountJobSkillAccount(int accountId);
        public Task<List<JobSkill>> ListJobSkillAvaliable(int accountId);
        public Task<AccountJobSkillView> AccountJobSkillDetail(int accountJobSkillId);
        public Task<AccountJobSkillView> AccountJobSkillAdd(AccountJobSkillAdd key);
    }
}
