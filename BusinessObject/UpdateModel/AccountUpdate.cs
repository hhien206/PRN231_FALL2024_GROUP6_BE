using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.UpdateModel
{
    public class AccountUpdate
    {
        public int AccountId {  get; set; }
        public string? FullName { get; set; }
        public string? Description {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Major {  get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? urlPicture {  get; set; }
        public string? gender { get; set; }
    }
}
