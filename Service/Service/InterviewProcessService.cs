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
    public class InterviewProcessService : IInterviewProcessService
    {
        public IInterviewProcessRepository InterviewProcessRepository;
        public InterviewProcessService()
        {
            this.InterviewProcessRepository = new InterviewProcessRepository();
        }
        public async Task<ServiceResult> ViewListInterviewProcessUser(int accountId)
        {
            try
            {
                var listInterviewProcess = await InterviewProcessRepository.ListInterviewProcessUser(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Interview Process",
                    Data = listInterviewProcess
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
        public async Task<ServiceResult> ViewListInterviewProcessHR(int accountId)
        {
            try
            {
                var listInterviewProcess = await InterviewProcessRepository.ListInterviewProcessHR(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Interview Process",
                    Data = listInterviewProcess
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
        public async Task<ServiceResult> ViewListInterviewProcessJob(int jobId)
        {
            try
            {
                var listInterviewProcess = await InterviewProcessRepository.ListInterviewProcessJob(jobId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Interview Process",
                    Data = listInterviewProcess
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
        public async Task<ServiceResult> ViewInterviewProcessDetail(int interviewProcessId)
        {
            try
            {
                var interviewProcess = await InterviewProcessRepository.InterviewProcessDetail(interviewProcessId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Interview Process",
                    Data = interviewProcess
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
        public async Task<ServiceResult> InterviewProcessAdd(InterviewProcessAdd key)
        {
            try
            {
                var interviewProcess = await InterviewProcessRepository.InterviewProcessAdd(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Add Success",
                    Data = interviewProcess
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
        public async Task<ServiceResult> InterviewProcessUpdate(InterviewProcessUpdate key)
        {
            try
            {
                var interviewProcess = await InterviewProcessRepository.InterviewProcessUpdate(key);
                if(interviewProcess == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = $"Not Found",
                    };
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Update Success",
                    Data = interviewProcess
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
