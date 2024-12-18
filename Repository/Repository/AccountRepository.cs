﻿using Repository.IRepository;
using Repository.LocalStorage;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using BusinessObject.UpdateModel;

namespace Repository.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        ICertificateRepository certiRepo;
        IRoleRepository roleRepo;
        IAccountJobSkillRepository accountJobSkillRepo;
        IEducateRepository educateRepo;
        public AccountRepository()
        {
            certiRepo = new CertificateRepository();
            roleRepo = new RoleRepository();
            accountJobSkillRepo = new AccountJobskillRepository();
            educateRepo = new EducateRepository();
        }
        public async Task<List<AccountView>> GetAllAccount(int sizePaging, int indexPaging)
        {
            try
            {
                var accounts = Paging((await GetAllAsync()), sizePaging, indexPaging);
                var result = await ConvertListAccountIntoListAccountView(accounts);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<AccountView>> GetAllAccountHR()
        {
            try
            {
                var accounts = (await GetAllAsync()).FindAll(l=>l.RoleId == 2);
                var result = await ConvertListAccountIntoListAccountView(accounts);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AccountView> GetAccountById(int accountId)
        {
            try
            {
                var account = GetById(accountId);
                var result = await ConvertAccountIntoAccountView(account);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AccountView?> AddAccount(AccountAdd key, int roleId)
        {
            try
            {
                var acc = (await GetAllAsync()).SingleOrDefault(l=>l.Email == key.email);
                if (acc != null) return null;
                Account account = new()
                {
                    Email = key.email,
                    PasswordHash = key.password,
                    FullName = key.FullName,
                    PhoneNumber = key.PhoneNumber,
                    Address = key.Address,
                    Major = null,
                    Gender = key.gender,
                    RoleId = roleId,
                };
                await CreateAsync(account);
                var result = await ConvertAccountIntoAccountView(account);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AccountView?> UpdateAccount(AccountUpdate key)
        {
            try
            {
                var account = GetById(key.AccountId);
                account.FullName = key.FullName;
                account.Description = key.Description;
                account.PhoneNumber = key.PhoneNumber;
                account.Address = key.Address;
                account.Major = key.Major;
                account.DateOfBirth = DateOnly.FromDateTime((DateTime)key.DateOfBirth);
                account.UrlPicture = key.urlPicture;
                await UpdateAsync(account);
                var result = await ConvertAccountIntoAccountView(account);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AccountView?> CheckAccount(string email, string password)
        {
            try
            {
                var listAccount = await GetAllAsync();
                var account = listAccount?.SingleOrDefault(l => l.Email == email && l.PasswordHash == password);
                if (account == null) return null;
                var result = await ConvertAccountIntoAccountView(account);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<AccountView>> ViewListAccount(int sizePaging,int indexPaging)
        {
            try
            {
                var list = await GetAllAsync();
                list = Paging(list, sizePaging, indexPaging);
                var result = await ConvertListAccountIntoListAccountView(list);
                return result;
            }
            catch(Exception) 
            {
                throw;
            }
        }
        public async Task<string> CreateConfirmTokenAccount(string email, string type)
        {
            try
            {
                if (type == "FORGET")
                {
                    var account = (await GetAllAsync()).SingleOrDefault(l => l.Email == email);
                    if (account == null) return "";
                    var token = CreateConfirmToken(6);
                    ListStorageAccount.AddAccountConfirm(account, token);
                    return token;
                }
                else if (type == "VERIFY")
                {
                    var account = new Account
                    {
                        Email = email,
                    };
                    var token = CreateConfirmToken(6);
                    ListStorageAccount.AddAccountVerify(account, token);
                    return token;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<AccountView> ForgetPasswork(string email, string password, string token)
        {
            try
            {
                var account = (await GetAllAsync()).SingleOrDefault(l => l.Email == email);
                if (ListStorageAccount.RemoveAccountConfirm(account, token) == true)
                {
                    account.PasswordHash = password;
                    await UpdateAsync(account);
                    var result = await ConvertAccountIntoAccountView(account);
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> VerifyAccount(string email, string token)
        {
            try
            {
                return ListStorageAccount.RemoveAccountVerify(email, token);
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

        private async Task<List<AccountView>> ConvertListAccountIntoListAccountView(List<Account> key)
        {
            List<AccountView> results = new List<AccountView>();
            foreach (var item in key)
            {
                results.Add(await ConvertAccountIntoAccountView(item));
            }
            return results;
        }
        private async Task<AccountView> ConvertAccountIntoAccountView(Account key)
        {
            var certificates = await certiRepo.ListCertificateAccount(key.AccountId);
            var educates = await educateRepo.ListEducateAccount(key.AccountId);
            RoleView? role = new RoleView();
            if (key.RoleId == null)
                role = null;
            else 
                role = await roleRepo.RoleDetail(key.RoleId);
            var accountJobSkill = await accountJobSkillRepo.ListAccountJobSkillAccount(key.AccountId);
            AccountView result = new AccountView();
            result.ConvertAccountIntoAccountView(key,role,certificates,accountJobSkill, educates);
            return result;
        }
    }
}
