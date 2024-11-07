using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Microsoft.Identity.Client;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class InterviewProcessRepository : GenericRepository<InterviewProcess>, IInterviewProcessRepository
    {
        IApplicationRepository appliRepo;
        IAccountRepository accountRepo;
        public InterviewProcessRepository()
        {
            appliRepo = new ApplicationRepository();
            accountRepo = new AccountRepository();
        }
        public async Task<List<InterviewProcessView>> ListInterviewProcessUser(int accountId)
        {
            try
            {
                var applications = (await appliRepo.GetAllAsync()).FindAll(l => l.AccountId == accountId);
                var listinterviewProcess = await GetAllAsync();
                List<InterviewProcess> result = new List<InterviewProcess>();
                foreach(var applicaiton in applications)
                {
                    var interview = listinterviewProcess.FirstOrDefault(l=>l.ApplicationId == applicaiton.ApplicationId);
                    if(interview != null)
                    {
                        result.Add(interview);
                    }
                }
                return await ConvertListInterviewProcessIntoListInterviewProcessView(result);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<InterviewProcessView>> ListInterviewProcessHR(int accountId)
        {
            try
            {
                var listinterviewProcess = (await GetAllAsync()).FindAll(l => l.AccountId == accountId);
                return await ConvertListInterviewProcessIntoListInterviewProcessView(listinterviewProcess);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<InterviewProcessView>> ListInterviewProcessJob(int jobId)
        {
            try
            {
                var listApplication = await appliRepo.ListApplicationJob(jobId);
                List<InterviewProcess> results = new();
                foreach (var item in listApplication)
                {
                    var listinterviewProcess = (await GetAllAsync()).FindAll(l => l.ApplicationId == item.ApplicationId);
                    results.AddRange(listinterviewProcess);
                }
                return await ConvertListInterviewProcessIntoListInterviewProcessView(results);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<InterviewProcessView> InterviewProcessDetail(int interviewProcessId)
        {
            try
            {
                var interviewProcess = GetById(interviewProcessId);
                return await ConvertInterviewProcessIntoInterviewProcessView(interviewProcess);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<InterviewProcessView> InterviewProcessAdd(InterviewProcessAdd key)
        {
            try
            {
                InterviewProcess interviewProcess = new()
                {
                    AccountId = key.HrId,
                    ApplicationId = key.ApplicationId,
                    RoundNumber = key.RoundNumber,
                    InterviewDate = key.InterviewDate,
                    Status = "PENDING"
                };
                await CreateAsync(interviewProcess);
                await appliRepo.ApplicationStatusUpdate(new ApplicationUpdate()
                {
                    ApplicationId = (int)interviewProcess.ApplicationId,
                    Status = "INTERVIEWING"
                });
                return await ConvertInterviewProcessIntoInterviewProcessView(interviewProcess);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<InterviewProcessView?> InterviewProcessUpdate(InterviewProcessUpdate key)
        {
            try
            {
                var interviewProcess = GetById(key.InterviewProcessId);
                if (interviewProcess == null) return null;
                interviewProcess.Detail = key.Detail;
                interviewProcess.Result = key.Result;
                interviewProcess.Status = key.Status;
                await UpdateAsync(interviewProcess);
                return await ConvertInterviewProcessIntoInterviewProcessView(interviewProcess);

            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<List<InterviewProcessView>> ConvertListInterviewProcessIntoListInterviewProcessView(List<InterviewProcess> key)
        {
            List<InterviewProcessView> results = new List<InterviewProcessView>();
            foreach (var item in key)
            {
                results.Add(await ConvertInterviewProcessIntoInterviewProcessView(item));
            }
            return results;
        }
        private async Task<InterviewProcessView> ConvertInterviewProcessIntoInterviewProcessView(InterviewProcess key)
        {
            var application = appliRepo.GetById(key.ApplicationId);
            var account = await accountRepo.GetAccountById((int)key.AccountId);
            InterviewProcessView result = new InterviewProcessView();
            result.ConvertInterviewProcessIntoInterviewProcessView(key, account ,application);
            return result;
        }
    }
}
