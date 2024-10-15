using BusinessObject.AddModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IResumeService
    {
        public Task<ServiceResult> ViewAllResumeAccount(int accountId);
        public Task<ServiceResult> ViewDetailResume(int resumeId);
        public Task<ServiceResult> AddResume(ResumeAdd key);
    }
}
