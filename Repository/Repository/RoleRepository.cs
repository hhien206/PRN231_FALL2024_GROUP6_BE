using BusinessObject.AddModel;
using BusinessObject.ViewModel;
using DataAccessObject.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository()
        {

        }
        public async Task<List<RoleView>> ListRole()
        {
            try
            {
                var listRole = await GetAllAsync();
                return await ConvertListRoleIntoListRoleView(listRole);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<RoleView?> RoleDetail(int? roleId)
        {
            try
            {
                if (roleId == null) return null;
                var role = GetById(roleId);
                RoleView result = new();
                result.ConvertRoleIntoRoleView(role);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
      
        private async Task<List<RoleView>> ConvertListRoleIntoListRoleView(List<Role> key)
        {
            List<RoleView> results = new List<RoleView>();
            foreach (var item in key)
            {
                RoleView result = new();
                result.ConvertRoleIntoRoleView(item);
                results.Add(result);
            }
            return results;
        }
    }
}
