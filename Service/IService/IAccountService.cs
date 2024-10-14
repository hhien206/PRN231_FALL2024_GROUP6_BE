using BusinessObject.ViewModel;
using BusinessObject.ViewModel.Request;
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
        public Task<ServiceResult> AddAccount(SignupRequest request);
        public Task<ServiceResult> LoginAccount(LoginRequest request);
        //public Task<ServiceResult> LoginToken(string accessToken);
        //public Task<ServiceResult> SendToken(string email);
        //public Task<ServiceResult> ResetPassword(string email, string password, string token);
        //public Task<ServiceResult> ViewListAccount(int sizePaging, int indexPaging);
    }
}
