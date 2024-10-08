using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Repository.IRepository;
using Repository.Repository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class JobCategoryService:IJobCategoryService
    {
        public IJobCategoryRepository cateRepository;
        public JobCategoryService()
        {
            this.cateRepository = new JobCategoryRepository();
        }
        public async Task<ServiceResult> ViewAllJobCategory(string? nameQuickSearch)
        {
            try
            {
                var listJobCategory = await cateRepository.ListJobCategoryQuickSearch(nameQuickSearch);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Category With Search '{nameQuickSearch}'",
                    Data = listJobCategory
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> ViewJobCategoryById(int categoryId)
        {
            try
            {
                var jobCategory = cateRepository.GetById(categoryId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Category",
                    Data = jobCategory
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddJobCategory(JobCategoryAdd key)
        {
            try
            {
                var category = new JobCategory
                {
                    Name = key.Name,
                    Description = key.Description,
                };
                await cateRepository.CreateAsync(category);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Category Success",
                    Data = category
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> UpdateJobCategory(int jobCategoryId, JobCategoryUpdate key)
        {
            try
            {
                var category = cateRepository.GetById(jobCategoryId);
                if (category == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Category Not Found",
                    };
                }
                category.Name = key.Name;
                category.Description = key.Description;
                await cateRepository.UpdateAsync(category);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Category Success",
                    Data = category
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> DeleteJobCategory(int jobCateId)
        {
            try
            {
                var cate = cateRepository.GetById(jobCateId);
                if (cate == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Category Not Found",
                    };
                }
                await cateRepository.RemoveAsync(cate);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Delete Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
