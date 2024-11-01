using DataAccessObject.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class ApplicationView
    {
        public int ApplicationId { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public string UrlCv { get; set; }

        public string Status { get; set; }

        public AccountView Account { get; set; }

        public JobView Job { get; set; }

        public void ConvertApplicationIntoApplicationView(Application key, AccountView account, JobView job)
        {
            ApplicationId = key.ApplicationId;
            ApplicationDate = key.ApplicationDate;
            UrlCv = key.UrlCv;
            Status = key.Status;
            Account = account;
            Job = job;
        }
    }
}
