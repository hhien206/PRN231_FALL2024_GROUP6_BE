using BusinessObject.AddModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICertificateService
    {
        public Task<ServiceResult> ViewAllCertificateAccount(int accountId);
        public Task<ServiceResult> ViewDetailCertificate(int certificateId);
        public Task<ServiceResult> AddCertificate(CertificateAdd key);
        public Task<ServiceResult> DeleteCertificate(int certificateId);
    }
}
