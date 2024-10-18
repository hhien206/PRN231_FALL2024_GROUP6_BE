using Repository.IRepository;
using Repository.Repository;
using DataAccessObject.Models;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using BusinessObject.AddModel;

namespace Service.Service
{
    public class AccountService : IAccountService
    {
        public IAccountRepository accountRepository;
        public AccountService()
        {
            this.accountRepository = new AccountRepository();
        }
        public async Task<ServiceResult> GetAllAccount(int sizePaging, int indexPaging)
        {
            try
            {
                var accounts = await accountRepository.GetAllAccount(sizePaging, indexPaging);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "List Account",
                    Data = accounts
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
        public async Task<ServiceResult> GetAccountById(int accountId)
        {
            try
            {
                var account = await accountRepository.GetAccountById(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Account",
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
        public async Task<ServiceResult> AddAccount(AccountAdd key, int roleId)
        {
            try
            {
                /*if ((await accountRepository.GetAllAsync()).SingleOrDefault(l => l.Email == key.email) != null)
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Email had exist!"
                    };*/
                var account = await accountRepository.AddAccount(key, roleId);
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
        public async Task<ServiceResult> LoginAccount(string email, string password)
        {
            try
            {
                var account = await accountRepository.CheckAccount(email, password);
                if (account == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Invalid Email or Password",
                    };
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Login Success",
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

        public async Task<ServiceResult> SendToken(string email, string type)
        {
            try
            {
                var token = await accountRepository.CreateConfirmTokenAccount(email, type);
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
                var account = await accountRepository.ForgetPasswork(email, password, token);
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
        public async Task<ServiceResult> VerifyAccount(string email, string token)
        {
            try
            {
                var result = await accountRepository.VerifyAccount(email, token);
                if (result == true)
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Verify Success",

                    };
                else
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Verify Fail",
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
