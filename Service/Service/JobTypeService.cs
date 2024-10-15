using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
using DataAccessObject.Models;
using Repository.IRepository;
using Repository.Repository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Service
{
    public class JobTypeService : IJobTypeService
    {
        public IJobTypeRepository typeRepository;
        public JobTypeService()
        {
            this.typeRepository = new JobTypeRepository();
        }
        public async Task<ServiceResult> ViewAllJobType(string? nameQuickSearch)
        {
            try
            {
                var listJobType = await typeRepository.ListJobTypeQuickSearch(nameQuickSearch);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Type With Search '{nameQuickSearch}'",
                    Data = listJobType
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
        public async Task<ServiceResult> ViewDetailJobType(int jobTypeId)
        {
            try
            {
                var jobType = typeRepository.GetById(jobTypeId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Job Type",
                    Data = jobType
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
        public async Task<ServiceResult> AddJobType(JobTypeAdd key)
        {
            try
            {
                var type = new JobType
                {
                    Name = key.Name,
                    Description = key.Description,
                };
                await typeRepository.CreateAsync(type);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Type Success",
                    Data = type
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
        public async Task<ServiceResult> UpdateJobType(int jobTypeId, JobTypeUpdate key)
        {
            try
            {
                var type = typeRepository.GetById(jobTypeId);
                if (type == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Type Not Found",
                    };
                }
                type.Name = key.Name;
                type.Description = key.Description;
                await typeRepository.UpdateAsync(type);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Type Success",
                    Data = type
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
        public async Task<ServiceResult> DeleteJobType(int jobTypeId)
        {
            try
            {
                var type = typeRepository.GetById(jobTypeId);
                if (type == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Type Not Found",
                    };
                }
                await typeRepository.RemoveAsync(type);
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
