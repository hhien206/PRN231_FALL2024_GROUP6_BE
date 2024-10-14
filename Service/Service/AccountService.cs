using Repository.IRepository;
using Repository.Repository;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using BusinessObject.ViewModel.Request;
using BC = global::BCrypt.Net.BCrypt;

namespace Service.Service
{
    public class AccountService : IAccountService
    {
        public IAccountRepository accountRepository;
        public AccountService()
        {
            this.accountRepository = new AccountRepository();
        }
        public async Task<ServiceResult> AddAccount(SignupRequest request)
        {
            try
            {
                Account account = new Account
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    PasswordHash = BC.EnhancedHashPassword(request.Password, 13),
                    RoleId = 3,
                    //AccessToken = key.accessToken,
                };
                await accountRepository.CreateAsync(account);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Account Success",
                    Data = account
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> LoginAccount(LoginRequest request)
        {
            try
            {
                var account = await accountRepository.CheckAccount(request);
                if(account != null)
                {
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Login Success",
                        Data = account
                    };
                }
                else
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Login Fail",
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        /*public async Task<ServiceResult> LoginToken(string accessToken)
        {
            try
            {
                var account = await accountRepository.CheckAccessToken(accessToken);
                if (account != null)
                {
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Login Success",
                        Data = account
                    };
                }
                else
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Login Fail",
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }*/
        public async Task<ServiceResult> SendToken(string email)
        {
            try
            {
                var token = await accountRepository.CreateConfirmTokenAccoutn(email);
                if (token != "")
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Send Success",
                        Data = token
                    };
                else               
                   return new ServiceResult
                    {
                        Status = 404,
                        Message = "Account Not Found",
                        Data = token
                    };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> ResetPassword(string email, string password, string token)
        {
            try
            {
                var account = await accountRepository.ForgetPasswork(email,password,token);
                if (account != null)
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Reset Success",
                        Data = account
                    };
                else
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Invalid Token",
                    };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> ViewListAccount(int sizePaging,int indexPaging)
        {
            try
            {
                var list = await accountRepository.ViewListAccount(sizePaging,indexPaging);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "List Account",
                    Data = list
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
