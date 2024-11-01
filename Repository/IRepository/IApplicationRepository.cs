using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IApplicationRepository : IGenericRepository<Application>
    {
        public Task<List<ApplicationView>> ListApplicationJob(int jobId);
        public Task<List<ApplicationView>> ListApplicationAccount(int accountId);
        public Task<ApplicationView> ApplicationDetail(int applicationId);
        public Task<ApplicationView> ApplicationAdd(ApplicationAdd key);
        public Task<ApplicationView> ApplicationStatusUpdate(ApplicationUpdate key);
        public Task RefuseAllApplicationInsufficient(int jobId);
    }
}
