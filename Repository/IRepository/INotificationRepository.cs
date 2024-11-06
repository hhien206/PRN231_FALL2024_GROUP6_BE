using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        public Task<int> GetNotificationUnseenQuantity(int accountId);
        public Task<List<NotificationView>> GetNotificationUser(int accountId);
        public Task<NotificationView> AddNotification(NotificationAdd key);
    }
}
