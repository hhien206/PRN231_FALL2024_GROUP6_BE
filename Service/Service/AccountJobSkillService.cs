using BusinessObject.AddModel;
using Repository.IRepository;
using Repository.Repository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AccountJobSkillService : IAccountJobSkillService
    {
        public IAccountJobSkillRepository AccountJobSkillRepository;
        public AccountJobSkillService()
        {
            this.AccountJobSkillRepository = new AccountJobskillRepository();
        }
        public async Task<ServiceResult> ViewAllAccountJobSkillAccount(int accountId)
        {
            try
            {
                var listAccountJobSkill = await AccountJobSkillRepository.ListAccountJobSkillAccount(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List AccountJobSkill",
                    Data = listAccountJobSkill
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }

        public async Task<ServiceResult> ViewAllJobSkillAvaliable(int accountId)
        {
            try
            {
                var listJobSkill = await AccountJobSkillRepository.ListJobSkillAvaliable(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Job Skill",
                    Data = listJobSkill
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }

        public async Task<ServiceResult> ViewDetailAccountJobSkill(int accountJobSkillId)
        {
            try
            {
                var accountJobSkill = AccountJobSkillRepository.AccountJobSkillDetail(accountJobSkillId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"AccountJobSkill",
                    Data = accountJobSkill
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddAccountJobSkill(AccountJobSkillAdd key)
        {
            try
            {
                var accountJobSkill = await AccountJobSkillRepository.AccountJobSkillAdd(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create AccountJobSkill Success",
                    Data = accountJobSkill
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> DeleteAccountJobSkill(int accountJobSkillId)
        {
            try
            {
                var accountJobSkill = await AccountJobSkillRepository.GetByIdAsync(accountJobSkillId);
                if(accountJobSkill == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found",
                    };
                }
                AccountJobSkillRepository.RemoveAsync(accountJobSkill);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Delete Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
