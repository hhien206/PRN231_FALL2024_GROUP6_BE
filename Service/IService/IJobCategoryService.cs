using BusinessObject.ViewModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IJobCategoryService
    {
        public Task<ServiceResult> ViewAllJobCategory(string? nameQuickSearch);
        public Task<ServiceResult> ViewJobCategoryById(int categoryId);
        public Task<ServiceResult> AddJobCategory(JobCategoryAdd key);
        public Task<ServiceResult> UpdateJobCategory(int jobCategoryId, JobCategoryUpdate key);
        public Task<ServiceResult> DeleteJobCategory(int jobCategoryId);
    }
}
