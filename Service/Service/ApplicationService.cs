using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
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
    public class ApplicationService : IApplicationService
    {
        public IApplicationRepository applicationRepository;
        public ApplicationService()
        {
            this.applicationRepository = new ApplicationRepository();
        }
        public async Task<ServiceResult> ViewApplicationAccount(int accountId)
        {
            try
            {
                var listApplication = await applicationRepository.ListApplicationAccount(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Application",
                    Data = listApplication
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
        public async Task<ServiceResult> ViewApplicationJob(int jobId)
        {
            try
            {
                var listApplication = await applicationRepository.ListApplicationJob(jobId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Application",
                    Data = listApplication
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
        public async Task<ServiceResult> ViewApplicationDetail(int applicationId)
        {
            try
            {
                var application = await applicationRepository.ApplicationDetail(applicationId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Application",
                    Data = application
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
        public async Task<ServiceResult> AddApplication(ApplicationAdd key)
        {
            try
            {
                var application = await applicationRepository.ApplicationAdd(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Application",
                    Data = application
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
        public async Task<ServiceResult> UpdateApplicationStatus(ApplicationUpdate key)
        {
            try
            {
                var application = await applicationRepository.ApplicationStatusUpdate(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Application",
                    Data = application
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
