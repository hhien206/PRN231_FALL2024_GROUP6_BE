using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IRoleService
    {
        public Task<ServiceResult> ViewListRole();
        public Task<ServiceResult> ViewRoleDetail(int roleId);
    }
}
