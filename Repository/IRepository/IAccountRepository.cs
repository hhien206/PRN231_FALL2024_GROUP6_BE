using BusinessObject.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Task<Account?> CheckAccount(string email, string password);
        //public Task<Account?> CheckAccessToken(string accessToken);
        public Task<string> CreateConfirmTokenAccoutn(string email);
        public Task<Account> ForgetPasswork(string email, string password, string token);
        public Task<List<Account>> ViewListAccount(int sizePaging, int indexPaging);
    }
}
