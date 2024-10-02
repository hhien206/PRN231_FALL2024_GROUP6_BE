using BusinessObject.ViewModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IJobLevelService
    {
        public Task<ServiceResult> ViewAllJobLevel(string? nameQuickSearch);
        public Task<ServiceResult> AddJobLevel(JobLevelAdd key);
        public Task<ServiceResult> UpdateJobLevel(int jobLevelId, JobLevelUpdate key);
        public Task<ServiceResult> DeleteJobLevel(int jobLevelId);
    }
}
