using BusinessObject.ViewModel;
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
        public Task<ServiceResult> AddAccount(AccountAdd key);
        public Task<ServiceResult> LoginAccount(string email, string password);
        public Task<ServiceResult> LoginToken(string accessToken);
        public Task<ServiceResult> SendToken(string email);
        public Task<ServiceResult> ResetPassword(string email, string password, string token);
    }
}
