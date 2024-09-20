using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.LocalStorage
{
    public class StorageAccount
    {
        public Account? account { get; set; }
        public string? confirmToken { get; set; }
    }
    public class ListStorageAccount
    {
        public static List<StorageAccount> listStorageAccountConfirm = new();
        public static void AddAccountConfirm(Account account, string token)
        {
            var acc = listStorageAccountConfirm.SingleOrDefault(l => l.account == account);
            if(acc == null)
            {
                listStorageAccountConfirm.Add(new StorageAccount
                {
                    account = account,
                    confirmToken = token,
                });
            }
            else
            {
                acc.confirmToken = token;
            }
        }
        public static bool RemoveAccountConfirm(Account account,string token)
        {
            //var acc = listStorageAccountConfirm.SingleOrDefault(l => l.account == account && l.confirmToken == token);
            var listAcc = listStorageAccountConfirm.FindAll(l => l.account.Email == account.Email);
            var acc = listAcc.SingleOrDefault(l =>l.confirmToken == token);
            if (acc != null)
            {
                listStorageAccountConfirm.Remove(acc);
                return true;
            }
            return false;
        }
    }
}
