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
    public class EducateService :IEducateService
    {
        public IEducateRepository EducateRepository;
        public EducateService()
        {
            this.EducateRepository = new EducateRepository();
        }
        public async Task<ServiceResult> ViewAllEducateAccount(int accountId)
        {
            try
            {
                var listEducate = await EducateRepository.ListEducateAccount(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Educate",
                    Data = listEducate
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
        public async Task<ServiceResult> ViewDetailEducate(int educateId)
        {
            try
            {
                var educate = EducateRepository.EducateDetail(educateId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Educate",
                    Data = educate
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
        public async Task<ServiceResult> AddEducate(EducateAdd key)
        {
            try
            {
                var educate = await EducateRepository.EducateAdd(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Educate Success",
                    Data = educate
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
        public async Task<ServiceResult> DeleteEducate(int educateId)
        {
            try
            {
                var educate = EducateRepository.GetById(educateId);
                if (educate == null)
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Not Found",
                    };
                await EducateRepository.RemoveAsync(educate);
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
