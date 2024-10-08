using BusinessObject.Models;
using DataAccessObject.ViewModel;
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
    public class JobLevelService : IJobLevelService
    {
        public IJobLevelRepository levelRepository;
        public JobLevelService()
        {
            this.levelRepository = new JobLevelRepository();
        }
        public async Task<ServiceResult> ViewAllJobLevel(string? nameQuickSearch)
        {
            try
            {
                var listJobLevel = await levelRepository.ListJobLevelQuickSearch(nameQuickSearch);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Level With Search '{nameQuickSearch}'",
                    Data = listJobLevel
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
        public async Task<ServiceResult> ViewDetailobLevel(int jobLevelId)
        {
            try
            {
                var jobLevel =  levelRepository.GetById(jobLevelId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Job Level",
                    Data = jobLevel
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
        public async Task<ServiceResult> AddJobLevel(JobLevelAdd key)
        {
            try
            {
                var level = new JobLevel
                {
                    Name = key.Name,
                    Description = key.Description,
                };
                await levelRepository.CreateAsync(level);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Level Success",
                    Data = level
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
        public async Task<ServiceResult> UpdateJobLevel(int jobLevelId, JobLevelUpdate key)
        {
            try
            {
                var level = levelRepository.GetById(jobLevelId);
                if (level == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Level Not Found",
                    };
                }
                level.Name = key.Name;
                level.Description = key.Description;
                await levelRepository.UpdateAsync(level);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Level Success",
                    Data = level
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
        public async Task<ServiceResult> DeleteJobLevel(int jobLevelId)
        {
            try
            {
                var level = levelRepository.GetById(jobLevelId);
                if (level == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Level Not Found",
                    };
                }
                await levelRepository.RemoveAsync(level);
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
