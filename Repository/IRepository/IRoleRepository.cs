using BusinessObject.ViewModel;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        public Task<List<RoleView>> ListRole();
        public Task<RoleView?> RoleDetail(int? roleId);
    }
}
