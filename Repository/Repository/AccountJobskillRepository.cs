using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AccountJobskillRepository : GenericRepository<AccountJobSkill>, IAccountJobSkillRepository
    {
        IJobSkillRepository skillRepo;
        public AccountJobskillRepository()
        {
            this.skillRepo = new JobSkillRepository();
        }
        public async Task<List<AccountJobSkillView>> ListAccountJobSkillAccount(int accountId)
        {
            try
            {
                var listAccountJobSkill = (await GetAllAsync()).FindAll(l => l.AccountId == accountId);
                return await ConvertListAccountJobSkillIntoListAccountJobSkillView(listAccountJobSkill);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<JobSkill>> ListJobSkillAvaliable(int accountId)
        {
            try
            {
                var listAccountJobSkill = (await GetAllAsync()).FindAll(l => l.AccountId == accountId);
                var listJobSkill = await skillRepo.GetAllAsync();
                foreach(var item in listAccountJobSkill)
                {
                    listJobSkill.RemoveAll(l => l.Id == item.JobSkillId);
                }
                return listJobSkill;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AccountJobSkillView> AccountJobSkillDetail(int accountJobSkillId)
        {
            try
            {
                var accountJobSkill = GetById(accountJobSkillId);
                AccountJobSkillView result = new();
                result = await ConvertAccountJobSkillIntoAccountJobSkillView(accountJobSkill);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AccountJobSkillView> AccountJobSkillAdd(AccountJobSkillAdd key)
        {
            try
            {
                AccountJobSkill AccountJobSkill = new()
                {
                    AccountId = key.AccountId,
                    JobSkillId = key.JobSkillId,
                    Experience = key.Experient
                };
                await CreateAsync(AccountJobSkill);
                AccountJobSkillView result = new();
                result = await ConvertAccountJobSkillIntoAccountJobSkillView(AccountJobSkill);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<List<AccountJobSkillView>> ConvertListAccountJobSkillIntoListAccountJobSkillView(List<AccountJobSkill> key)
        {
            List<AccountJobSkillView> results = new List<AccountJobSkillView>();
            foreach (var item in key)
            {
                results.Add(await ConvertAccountJobSkillIntoAccountJobSkillView(item));
            }
            return results;
        }
        private async Task<AccountJobSkillView> ConvertAccountJobSkillIntoAccountJobSkillView(AccountJobSkill key)
        {
            var skill = skillRepo.GetById(key.JobSkillId);
            AccountJobSkillView result = new();
            result.ConvertAccountJobSkillIntoAccountJobSkillView(key,skill);

            return result;
        }
    }
}
