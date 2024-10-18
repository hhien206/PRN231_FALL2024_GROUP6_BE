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
        public ApplicationRepository()
        {

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
    }
}
