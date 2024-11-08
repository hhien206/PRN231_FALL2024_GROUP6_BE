using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessObject.ViewModel
{
    public class InterviewProcessView
    {
        public int InterviewProcessId {  get; set; }
        public int? RoundNumber {  get; set; }
        public DateTime? InterviewDate { get; set; }
        public string? Detail {  get; set; }
        public string? Result { get; set; }
        public string? Status { get; set; }
        public AccountView? HRAccount {  get; set; }
        public ApplicationView? Application { get; set; }
        public void ConvertInterviewProcessIntoInterviewProcessView(InterviewProcess key,AccountView account, ApplicationView? application)
        {
            InterviewProcessId = key.InterviewProcessId;
            RoundNumber = key.RoundNumber;
            InterviewDate = key.InterviewDate;
            Detail = key.Detail;
            Result = key.Result;
            Status = key.Status;
            HRAccount = account;
            Application=application;
        }
    }
}
