using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ResumeRepository : GenericRepository<Resume>, IResumeRepository
    {
        public ResumeRepository()
        {

        }
        public async Task<List<ResumeView>> ListResumeAccount(int accountId)
        {
            try
            {
                var listResume = (await GetAllAsync()).FindAll(l => l.AccountId == accountId);
                return await ConvertListResumeIntoListResumeView(listResume);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResumeView> ResumeDetail(int ResumeId)
        {
            try
            {
                var resume = GetById(ResumeId);
                ResumeView result = new();
                result.ConvertResumeIntoResumeView(resume);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResumeView> ResumeAdd(ResumeAdd key)
        {
            try
            {
                Resume resume = new()
                {
                    UrlResume = key.UrlResume,
                    AccountId = key.AccountId,
                };
                await CreateAsync(resume);
                ResumeView result = new();
                result.ConvertResumeIntoResumeView(resume);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<List<ResumeView>> ConvertListResumeIntoListResumeView(List<Resume> key)
        {
            List<ResumeView> results = new List<ResumeView>();
            foreach (var item in key)
            {
                ResumeView result = new();
                result.ConvertResumeIntoResumeView(item);
                results.Add(result);
            }
            return results;
        }
    }
}
