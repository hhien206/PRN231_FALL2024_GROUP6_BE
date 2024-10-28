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
    public class EducateRepository : GenericRepository<Educate>, IEducateRepository
    {
        public EducateRepository()
        {

        }
        public async Task<List<EducateView>> ListEducateAccount(int accountId)
        {
            try
            {
                var listEducate = (await GetAllAsync()).FindAll(l=>l.AccountId == accountId);
                return await ConvertListEducateIntoListEducateView(listEducate);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<EducateView> EducateDetail(int educateId)
        {
            try
            {
                var educate = GetById(educateId);
                EducateView result = new();
                result.ConvertEducateIntoEducateView(educate);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<EducateView> EducateAdd(EducateAdd key)
        {
            try
            {
                Educate educate = new()
                {
                    EducateName = key.EducateName,
                    Description = key.Description,
                    Date = DateOnly.FromDateTime(key.Date),
                    AccountId = key.AccountId,
                };
                await CreateAsync(educate);
                EducateView result = new();
                result.ConvertEducateIntoEducateView(educate);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<List<EducateView>> ConvertListEducateIntoListEducateView(List<Educate> key)
        {
            List<EducateView> results = new List<EducateView>();
            foreach (var item in key)
            {
                EducateView result = new();
                result.ConvertEducateIntoEducateView(item);
                results.Add(result);
            }
            return results;
        }
    }
}
