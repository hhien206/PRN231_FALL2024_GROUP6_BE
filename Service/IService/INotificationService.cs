using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface INotificationService
    {
        public Task<ServiceResult> ViewNotificationUnseenQuantity(int accountId);
        public Task<ServiceResult> ViewListNotificationUser(int accountId);
    }
}
