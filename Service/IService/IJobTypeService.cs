using BusinessObject.ViewModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IJobTypeService
    {
        public Task<ServiceResult> ViewAllJobType(string? nameQuickSearch);
        public Task<ServiceResult> AddJobType(JobTypeAdd key);
        public Task<ServiceResult> UpdateJobType(int jobTypeId, JobTypeUpdate key);
        public Task<ServiceResult> DeleteJobType(int jobTypeId);
    }
}
