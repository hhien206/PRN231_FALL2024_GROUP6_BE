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
        public async Task<List<ApplicationView>> ListApplicationJob(int jobId)
        {
            try
            {
                var listApplication = (await GetAllAsync()).FindAll(l => l.JobId == jobId && l.Status == "PENDING");
                return await ConvertListApplicationIntoListApplicationView(listApplication);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ApplicationView>> ListApplicationAccount(int accountId)
        {
            try
            {
                var listApplication = (await GetAllAsync()).FindAll(l => l.AccountId == accountId);
                return await ConvertListApplicationIntoListApplicationView(listApplication);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ApplicationView> ApplicationDetail(int applicationId)
        {
            try
            {
                var application = GetById(applicationId);
                return await ConvertApplicationIntoApplicationView(application);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ApplicationView> ApplicationAdd(ApplicationAdd key)
        {
            try
            {
                var app = (await GetAllAsync()).FirstOrDefault(l => l.JobId == key.JobId && l.AccountId == key.AccountId);
                if (app != null)
                {
                    return new ApplicationView
                    {
                        Status = "ALREADY"
                    };
                }

                if ((await jobRepository.CheckQuantity((int)key.JobId)) == false)
                {
                    return new ApplicationView
                    {
                        Status = "FULL"
                    };
                }
                    
                Application application = new()
                {
                    ApplicationDate = DateTime.Now,
                    UrlCv = key.UrlCV,
                    Status = "PENDING",
                    AccountId = key.AccountId,
                    JobId = key.JobId
                };
                await jobRepository.IncreaseCurrentQuantityByOne((int)application.JobId);
                await CreateAsync(application);
                return await ConvertApplicationIntoApplicationView(application);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ApplicationView> ApplicationStatusUpdate(ApplicationUpdate key)
        {
            try
            {
                var application = GetById(key.ApplicationId);
                application.Status = key.Status;
                await UpdateAsync(application);
                return await ConvertApplicationIntoApplicationView(application);
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
        private async Task<List<ApplicationView>> ConvertListApplicationIntoListApplicationView(List<Application> key)
        {
            List<ApplicationView> results = new List<ApplicationView>();
            foreach (var item in key)
            {
                results.Add(await ConvertApplicationIntoApplicationView(item));
            }
            return results;
        }
        private async Task<ApplicationView> ConvertApplicationIntoApplicationView(Application key)
        {
            var account = await accountRepository.GetAccountById((int)key.AccountId);
            var job = await jobRepository.ViewJobDetail((int)key.JobId);
            ApplicationView result = new ApplicationView();
            result.ConvertApplicationIntoApplicationView(key, account, job);
            return result;
        }
    }
}
