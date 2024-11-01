using BusinessObject.AddModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAccountJobSkillService
    {
        public Task<ServiceResult> ViewAllAccountJobSkillAccount(int accountId);
        public Task<ServiceResult> ViewAllJobSkillAvaliable(int accountId);
        public Task<ServiceResult> ViewDetailAccountJobSkill(int AccountJobSkillId);
        public Task<ServiceResult> AddAccountJobSkill(AccountJobSkillAdd key);
        public Task<ServiceResult> DeleteAccountJobSkill(int accountJobSkillId);
    }
}
