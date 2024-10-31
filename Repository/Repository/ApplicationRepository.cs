using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
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
    public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
    {
        IAccountRepository accountRepository;
        IJobRepository jobRepository;
        IJobJobSkillRepository jobJobSkillRepository;
        IAccountJobSkillRepository accountJobSkillRepository;
        public ApplicationRepository()
        {
            accountRepository = new AccountRepository();
            jobRepository = new JobRepository();
            jobJobSkillRepository = new JobJobSkillRepository();
            accountJobSkillRepository = new AccountJobskillRepository();
        }
        public async Task<List<Application>> ListApplicationJob(int jobId)
        {
            try
            {
                var listApplication = (await GetAllAsync()).FindAll(l => l.JobId == jobId);
                return listApplication;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Application>> ListApplicationAccount(int accountId)
        {
            try
            {
                var listApplication = (await GetAllAsync()).FindAll(l => l.AccountId == accountId);
                return listApplication;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Application> ApplicationDetail(int applicationId)
        {
            try
            {
                var application = GetById(applicationId);
                return application;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Application> ApplicationAdd(ApplicationAdd key)
        {
            try
            {
                Application application = new()
                {
                    ApplicationDate = DateTime.Now,
                    UrlCv = key.UrlCV,
                    Status = "PENDING",
                    AccountId = key.AccountId,
                    JobId = key.JobId
                };
                await CreateAsync(application);
                return application;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Application> ApplicationStatusUpdate(ApplicationUpdate key)
        {
            try
            {
                var application = GetById(key.ApplicationId);
                application.Status = key.Status;
                await UpdateAsync(application);
                return application;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task RefuseAllApplicationInsufficient(int jobId)
        {
            var applications = (await GetAllAsync()).FindAll(l=>l.JobId == jobId && l.Status == "PENDING");
            var jobJobSkills = await jobJobSkillRepository.GetAllJobJobSkillByJobId(jobId);
            List<Account> accounts = new List<Account>();
            foreach(var application in applications)
            {
                accounts.Add(accountRepository.GetById(application.AccountId));
            }
            foreach (var account in accounts)
            {
                var accountJobSkills = (await accountJobSkillRepository.GetAllAsync()).FindAll(l=>l.AccountId == account.AccountId);
                foreach (var jobJobSkill in jobJobSkills)
                {
                    var accountJobSkill = accountJobSkills.FirstOrDefault(l => l.JobSkillId == jobJobSkill.JobSkillId);
                    if(accountJobSkill != null)
                    {
                        if(int.Parse(accountJobSkill.Experience) < int.Parse(jobJobSkill.ExperienceRequired))
                        {
                            var application = (await GetAllAsync()).FirstOrDefault(l => l.AccountId == account.AccountId && l.JobId == jobJobSkill.JobId && l.Status == "PENDING");
                            if(application != null)
                            {
                                application.Status = "REFUSED";
                                await UpdateAsync(application);
                                break;
                            }
                        }
                    }
                    else
                    {
                        var application = (await GetAllAsync()).FirstOrDefault(l => l.AccountId == account.AccountId && l.JobId == jobJobSkill.JobId && l.Status == "PENDING");
                        if (application != null)
                        {
                            application.Status = "REFUSED";
                            await UpdateAsync(application);
                            break;
                        }
                    }
                }
            }
        }
    }
}
