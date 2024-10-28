using BusinessObject.AddModel;
using BusinessObject.UpdateModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAccountService
    {
        public Task<ServiceResult> GetAllAccount(int sizePaging, int indexPaging);
        public Task<ServiceResult> GetAccountById(int accountId);
        public Task<ServiceResult> AddAccount(AccountAdd key, int roleId);
        public Task<ServiceResult> UpdateAccount(AccountUpdate key);
        public Task<ServiceResult> LoginAccount(string email, string password);
        public Task<ServiceResult> SendToken(string email, string type);
        public Task<ServiceResult> ResetPassword(string email, string password, string token);
        public Task<ServiceResult> VerifyAccount(string email, string token);
    }
}
