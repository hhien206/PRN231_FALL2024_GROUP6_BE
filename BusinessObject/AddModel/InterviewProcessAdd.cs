using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.AddModel
{
    public class InterviewProcessAdd
    {
        public int RoundNumber {  get; set; }
        public DateTime InterviewDate { get; set; }
        public int HrId {  get; set; }
        public int ApplicationId {  get; set; }
    }
}
