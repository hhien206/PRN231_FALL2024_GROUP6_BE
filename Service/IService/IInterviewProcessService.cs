using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IInterviewProcessService
    {
        public Task<ServiceResult> ViewListInterviewProcessUser(int accountId);
        public Task<ServiceResult> ViewListInterviewProcessHR(int accountId);
        public Task<ServiceResult> ViewListInterviewProcessJob(int jobId);
        public Task<ServiceResult> ViewInterviewProcessDetail(int interviewProcessId);
        public Task<ServiceResult> InterviewProcessAdd(InterviewProcessAdd key);
        public Task<ServiceResult> InterviewProcessUpdate(InterviewProcessUpdate key);
    }
}
