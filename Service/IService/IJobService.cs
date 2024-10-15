using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IJobService
    {
        public Task<ServiceResult> ViewAllJob();
        public Task<ServiceResult> ViewJobSearch(int sizePaging, int indexPaging, JobSearch key);
        public Task<ServiceResult> ViewJobDetail(int jobId);
        public Task<ServiceResult> AddJob(JobAdd key);
    }
}
