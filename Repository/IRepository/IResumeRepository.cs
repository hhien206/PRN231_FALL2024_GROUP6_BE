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
    public interface IResumeRepository : IGenericRepository<Resume>
    {
        public Task<List<ResumeView>> ListResumeAccount(int accountId);
        public Task<ResumeView> ResumeDetail(int ResumeId);
        public Task<ResumeView> ResumeAdd(ResumeAdd key);
    }
}
