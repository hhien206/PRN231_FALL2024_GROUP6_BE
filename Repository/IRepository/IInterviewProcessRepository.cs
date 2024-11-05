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
    public interface IInterviewProcessRepository : IGenericRepository<InterviewProcess>
    {
        public Task<List<InterviewProcessView>> ListInterviewProcessUser(int accountId);
        public Task<List<InterviewProcessView>> ListInterviewProcessHR(int accountId);
        public Task<List<InterviewProcessView>> ListInterviewProcessJob(int jobId);
        public Task<InterviewProcessView> InterviewProcessAdd(InterviewProcessAdd key);
        public Task<InterviewProcessView?> InterviewProcessUpdate(InterviewProcessUpdate key);
    }
}
