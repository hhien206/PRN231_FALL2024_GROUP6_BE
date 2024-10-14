using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel.Response
{
    public class GetAccountByIdResponse
    {
        public int AccountId { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string UrlPicture { get; set; }

        public string Gender { get; set; }

        public DateTime? DateCreate { get; set; }

        public int? RoleId { get; set; }

    }
}
