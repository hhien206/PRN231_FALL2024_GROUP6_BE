using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        public Task<List<JobView>> GetAllJob();
        public Task<List<JobView>> GetAllJobByAccount(int accountId);
        public Task<JobView?> AddJob(JobAdd key);
        public Task<List<JobView>?> ViewListJob(int sizePaging, int indexPaging, JobSearch key);
        public Task<JobView?> ViewJobDetail(int jobId);
    }
}
