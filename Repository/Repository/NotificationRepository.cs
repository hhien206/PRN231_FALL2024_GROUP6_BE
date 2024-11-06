using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        IAccountRepository accountRepository;
        public NotificationRepository()
        {
            accountRepository = new AccountRepository();
        }
        public async Task<int> GetNotificationUnseenQuantity(int accountId)
        {
            var notifications = (await GetAllAsync()).FindAll(l => l.AccountId == accountId && l.Status == "UNSEEN");
            return notifications.Count;
        }
        public async Task<List<NotificationView>> GetNotificationUser(int accountId)
        {
            var notifications = (await GetAllAsync()).FindAll(l=>l.AccountId == accountId);
            var listTemp = notifications.FindAll(l => l.Status == "UNSEEN");
            foreach (var item in listTemp)
            {
                item.Status = "SEEN";
                await UpdateAsync(item);
            }
            foreach (var item in listTemp)
            {
                item.Status = "UNSEEN";
            }
            return (await ConvertListNotificationIntoListNotificationView(notifications)).OrderByDescending(l=>l.NotificationId).ToList();
        }
        public async Task<NotificationView> AddNotification(NotificationAdd key)
        {
            Notification notification = new Notification()
            {
                AccountId = key.AccountId,
                Content = key.Content,
                Status = "UNSEEN"
            };
            await CreateAsync(notification);
            return await ConvertNotificationIntoNotificationView(notification);
        }
        private async Task<List<NotificationView>> ConvertListNotificationIntoListNotificationView(List<Notification> key)
        {
            List<NotificationView> results = new List<NotificationView>();
            foreach (var item in key)
            {
                NotificationView result = new();
                result = await ConvertNotificationIntoNotificationView(item);
                results.Add(result);
            }
            return results;
        }
        private async Task<NotificationView> ConvertNotificationIntoNotificationView(Notification key)
        {
            var account = await accountRepository.GetAccountById((int)key.AccountId);
            NotificationView result = new NotificationView();
            result.ConvertNotificationIntoNotificationView(key, account);
            return result;
        }
    }
}
