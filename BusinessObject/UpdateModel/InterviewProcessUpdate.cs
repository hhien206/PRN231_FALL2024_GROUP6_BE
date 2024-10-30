using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.UpdateModel
{
    public class InterviewProcessUpdate
    {
        public int InterviewProcessId {  get; set; }
        public string? Detail {  get; set; }
        public string? Result {  get; set; }
        public string? Status { get; set; }
    }
}
