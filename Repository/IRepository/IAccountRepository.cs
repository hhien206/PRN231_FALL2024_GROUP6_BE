using Repository.Repository;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.AddModel;
using BusinessObject.ViewModel;

namespace Repository.IRepository
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Task<List<AccountView>> GetAllAccount(int sizePaging, int indexPaging);
        public Task<AccountView> GetAccountById(int accountId);
        public Task<AccountView?> AddAccount(AccountAdd key, int roleId);
        public Task<AccountView?> CheckAccount(string email, string password);
        public Task<string> CreateConfirmTokenAccount(string email, string type);
        public Task<AccountView> ForgetPasswork(string email, string password, string token);
        public Task<bool> VerifyAccount(string email, string token);
    }
}
