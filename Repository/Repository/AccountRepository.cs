using Repository.IRepository;
using Repository.LocalStorage;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {

        public AccountRepository()
        {

        }
        public async Task<Account?> CheckAccount(string email,string password)
        {
            try
            {
                var listAccount = await GetAllAsync();
                return listAccount?.SingleOrDefault(l => l.Email == email && l.PasswordHash == password);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Account?> CheckAccessToken(string accessToken)
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
        }
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
