using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel.Request
{
    public class CreateAccountRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string Gender { get; set; }

        public int? RoleId { get; set; }

    }
}
