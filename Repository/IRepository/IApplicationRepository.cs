using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
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
        public Task<List<Application>> ListApplicationJob(int jobId);
        public Task<List<Application>> ListApplicationAccount(int accountId);
        public Task<Application> ApplicationDetail(int applicationId);
        public Task<Application> ApplicationAdd(ApplicationAdd key);
        public Task<Application> ApplicationStatusUpdate(ApplicationUpdate key);
    }
}
