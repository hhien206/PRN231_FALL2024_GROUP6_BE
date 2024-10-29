using BusinessObject.AddModel;
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
    public class CertificateService : ICertificateService
    {
        public ICertificateRepository certificateRepository;
        public CertificateService()
        {
            this.certificateRepository = new CertificateRepository();
        }
        public async Task<ServiceResult> ViewAllCertificateAccount(int accountId)
        {
            try
            {
                var listCertificate = await certificateRepository.ListCertificateAccount(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Certificate",
                    Data = listCertificate
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
        public async Task<ServiceResult> ViewDetailCertificate(int certificateId)
        {
            try
            {
                var certificate = certificateRepository.CertificateDetail(certificateId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Certificate",
                    Data = certificate
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
        public async Task<ServiceResult> AddCertificate(CertificateAdd key)
        {
            try
            {
                var certificate =  await certificateRepository.CertificateAdd(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Certificate Success",
                    Data = certificate
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
        public async Task<ServiceResult> DeleteCertificate(int certificateId)
        {
            try
            {
                var certificate = certificateRepository.GetById(certificateId);
                if(certificate == null)
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Not Found",
                    };
                await certificateRepository.RemoveAsync(certificate);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Certificate Success",
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
