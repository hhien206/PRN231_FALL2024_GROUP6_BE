using Repository.IRepository;
using Repository.LocalStorage;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = global::BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using BusinessObject.ViewModel.Request;

namespace Repository.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository()
        {
        }

        public async Task<Account?> CheckAccount(LoginRequest request)
        {
            try
            {
                // Retrieve the account matching the provided email
                var account = (await GetAllAsync()).SingleOrDefault(a => a.Email == request.Email);

                if (account == null) return null; // Return null if the account doesn't exist

                // Verify the password hash using BCrypt
                if (!BC.EnhancedVerify(request.Password, account.PasswordHash)) return null;

                return account; // Return the account if everything matches
            }
            catch (Exception)
            {
                throw; // Rethrow the exception to be handled by higher layers
            }
        }
        /*public async Task<Account?> CheckAccessToken(string accessToken)
        {
            try
            {
                var listAccount = await GetAllAsync();
                return listAccount?.SingleOrDefault(l => l.AccessToken == accessToken);
            }
            catch (Exception)
            {
                throw;
            }
        }*/
        public async Task<string> CreateConfirmTokenAccoutn(string email)
        {
            try
            {
                var account = (await GetAllAsync()).SingleOrDefault(l => l.Email == email);
                if (account == null) return "";
                var token = CreateConfirmToken(6);
                ListStorageAccount.AddAccountConfirm(account, token);
                return token;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Account> ForgetPasswork(string email,string password,string token)
        {
            try
            {
                var account = (await GetAllAsync()).SingleOrDefault(l => l.Email == email);
                if(ListStorageAccount.RemoveAccountConfirm(account, token) == true)
                {
                    account.PasswordHash = password;
                    await UpdateAsync(account);
                    return account;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }
        private string CreateConfirmToken(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] code = new char[length];

            for (int i = 0; i < length; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }
            return new string(code);
        }
        public async Task<List<Account>> ViewListAccount(int sizePaging,int indexPaging)
        {
            try
            {
                var list = await GetAllAsync();
                list = Paging(list, sizePaging, indexPaging);
                return list;
            }
            catch(Exception) 
            {
                throw;
            }
        }
    }
}
