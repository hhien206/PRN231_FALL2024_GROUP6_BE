using Repository.IRepository;
using Repository.Repository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class NotificationService : INotificationService
    {
        public INotificationRepository notificationRepository;
        public NotificationService()
        {
            this.notificationRepository = new NotificationRepository();
        }
        public async Task<ServiceResult> ViewNotificationUnseenQuantity(int accountId)
        {
            try
            {
                var notifications = await notificationRepository.GetNotificationUnseenQuantity(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Quantity",
                    Data = notifications
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
        public async Task<ServiceResult> ViewListNotificationUser(int accountId)
        {
            try
            {
                var notifications = await notificationRepository.GetNotificationUser(accountId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Notification",
                    Data = notifications
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
