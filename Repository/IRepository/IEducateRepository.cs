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
    public interface IEducateRepository : IGenericRepository<Educate>
    {
        public Task<List<EducateView>> ListEducateAccount(int accountId);
        public Task<EducateView> EducateDetail(int educateId);
        public Task<EducateView> EducateAdd(EducateAdd key);
    }
}
