using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class RoleView
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public void ConvertRoleIntoRoleView(Role key)
        {
            RoleId = key.RoleId;
            RoleName = key.RoleName;
        }
    }
}
