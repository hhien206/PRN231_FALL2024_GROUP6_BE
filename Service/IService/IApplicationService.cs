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
    public interface IApplicationService
    {
        public Task<ServiceResult> ViewApplicationAccount(int accountId);
        public Task<ServiceResult> ViewApplicationJob(int jobId);
        public Task<ServiceResult> ViewApplicationDetail(int applicationId);
        public Task<ServiceResult> AddApplication(ApplicationAdd key);
        public Task<ServiceResult> UpdateApplicationStatus(ApplicationUpdate key);
        public Task<ServiceResult> RefuseAllApplicationInsufficient(int jobId);
    }
}
