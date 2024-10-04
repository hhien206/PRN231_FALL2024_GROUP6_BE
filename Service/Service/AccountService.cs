//using Repository.IRepository;
//using Repository.Repository;
//using BusinessObject.ViewModel;
//using DataAccessObject.Models;
//using Service.IService;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Security.Principal;

//namespace Service.Service
//{
//    public class AccountService : IAccountService
//    {
//        public IAccountRepository accountRepository;
//        public AccountService()
//        {
//            this.accountRepository = new AccountRepository();
//        }
//        public async Task<ServiceResult> ResetPassword(string email, string password, string token)
//        {
//            try
//            {
//                var account = await accountRepository.ForgetPasswork(email, password, token);
//                if (account != null)
//                    return new ServiceResult
//                    {
//                        Status = 200,
//                        Message = "Reset Success",
//                        Data = account
//                    };
//                else
//                    return new ServiceResult
//                    {
//                        Status = 400,
//                        Message = "Invalid Token",
//                    };
//            }
//            catch (Exception ex)
//            {
//                return new ServiceResult
//                {
//                    Status = 501,
//                    Message = ex.ToString(),
//                };
//            }
//        }
//        public async Task<ServiceResult> ViewListAccount(int sizePaging, int indexPaging)
//        {
//            try
//            {
//                var list = await accountRepository.ViewListAccount(sizePaging, indexPaging);
//                return new ServiceResult
//                {
//                    Status = 200,
//                    Message = "Create Account Success",
//                    Data = list
//                };
//            }
//            catch (Exception ex)
//            {
//                return new ServiceResult
//                {
//                    Status = 501,
//                    Message = ex.ToString(),
//                };
//            }
//        }
//    }
//}
