﻿using BusinessObject.AddModel;
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
        public InterviewProcessRepository()
        {
            appliRepo = new ApplicationRepository();
        }
        public async Task<List<InterviewProcessView>> ListInterviewProcessUser(int accountId)
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
            InterviewProcessView result = new InterviewProcessView();
            result.ConvertInterviewProcessIntoInterviewProcessView(key, application);
            return result;
        }
    }
}
