using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel.Response
{
    public class AccountInfoResponse
    {
        public int AccountId { get; set; }

        public string Email { get; set; }

        public int? RoleId { get; set; }

    }
}
