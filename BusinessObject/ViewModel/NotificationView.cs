using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class NotificationView
    {
        public int NotificationId { get; set; }

        public string? Content { get; set; }

        public string? Status { get; set; }
        public AccountView? Account { get; set; }
        public void ConvertNotificationIntoNotificationView(Notification key, AccountView account)
        {
            NotificationId = key.NotificationId;
            Content = key.Content;
            Status = key.Status;
            Account = account;
        }
    }
}
