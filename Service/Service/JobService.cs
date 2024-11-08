using BusinessObject.AddModel;
using BusinessObject.ViewModel;
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
    public class JobService : IJobService
    {
        public IJobRepository jobRepository;
        public JobService()
        {
            this.jobRepository = new JobRepository();
        }
        public async Task<ServiceResult> ViewAllJob()
        {
            try
            {
                var jobs = await jobRepository.GetAllJob();
                return new ServiceResult
                {
                    Status = 200,
                    Message = "List Job",
                    Data = jobs
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

        public async Task<ServiceResult> ViewAllJobAccount(int accountId)
        {
            try
            {
                var jobs = await jobRepository.GetAllJobByAccount(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "List Job",
                    Data = jobs
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
        public async Task<ServiceResult> ViewJobSearch(int sizePaging, int indexPaging, JobSearch key)
        {
            try
            {
                var jobs = await jobRepository.ViewListJob(sizePaging, indexPaging, key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "List Job",
                    Data = jobs
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
        public async Task<ServiceResult> ViewJobDetail(int jobId)
        {
            try
            {
                var job = await jobRepository.ViewJobDetail(jobId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Job",
                    Data = job
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
        public async Task<ServiceResult> AddJob(JobAdd key)
        {
            try
            {
                if(key.Deadline < DateTime.Now)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Deadline cannot be in the past.",
                    };
                }
                var job = await jobRepository.AddJob(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Job Success",
                    Data = job
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
