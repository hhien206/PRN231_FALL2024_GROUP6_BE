using BusinessObject.AddModel;
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
    public class ResumeService : IResumeService
    {
        public IResumeRepository resumeRepository;
        public ResumeService()
        {
            this.resumeRepository = new ResumeRepository();
        }
        public async Task<ServiceResult> ViewAllResumeAccount(int accountId)
        {
            try
            {
                var listResume = await resumeRepository.ListResumeAccount(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Resume",
                    Data = listResume
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
        public async Task<ServiceResult> ViewDetailResume(int resumeId)
        {
            try
            {
                var resume = resumeRepository.ResumeDetail(resumeId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Resume",
                    Data = resume
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
        public async Task<ServiceResult> AddResume(ResumeAdd key)
        {
            try
            {
                var resume = await resumeRepository.ResumeAdd(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Resume Success",
                    Data = resume
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
