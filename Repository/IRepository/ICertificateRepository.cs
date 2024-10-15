using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICertificateRepository : IGenericRepository<Certificate>
    {
        public Task<List<CertificateView>> ListCertificateAccount(int accountId);
        public Task<CertificateView> CertificateDetail(int certificateId);
        public Task<CertificateView> CertificateAdd(CertificateAdd key);
    }
}
