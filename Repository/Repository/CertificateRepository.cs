using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Microsoft.Identity.Client;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CertificateRepository : GenericRepository<Certificate>, ICertificateRepository
    {
        public CertificateRepository()
        {

        }
        public async Task<List<CertificateView>> ListCertificateAccount(int accountId)
        {
            try
            {
                var listCertificate = (await GetAllAsync()).FindAll(l=>l.AccountId == accountId);
                return await ConvertListCertificateIntoListCertificateView(listCertificate);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CertificateView> CertificateDetail(int certificateId)
        {
            try
            {
                var certificate = GetById(certificateId);
                CertificateView result = new();
                result.ConvertCertificateIntoCertificateView(certificate);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CertificateView> CertificateAdd(CertificateAdd key)
        {
            try
            {
                Certificate certificate = new()
                {
                    CertificateUrl = key.UrlCertificate,
                    AccountId = key.AccountId,
                };
                await CreateAsync(certificate);
                CertificateView result = new();
                result.ConvertCertificateIntoCertificateView(certificate);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<List<CertificateView>> ConvertListCertificateIntoListCertificateView(List<Certificate> key)
        {
            List<CertificateView> results = new List<CertificateView>();
            foreach (var item in key)
            {
                CertificateView result = new();
                result.ConvertCertificateIntoCertificateView(item);
                results.Add(result);
            }
            return results;
        }
    }
}
