using Repository.IRepository;
using Repository.Repository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class RoleService : IRoleService
    {
        public IRoleRepository roleRepository;
        public RoleService()
        {
            this.roleRepository = new RoleRepository();
        }
        public async Task<ServiceResult> ViewListRole()
        {
            try
            {
                var listRole = await roleRepository.ListRole();
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Role",
                    Data = listRole
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> ViewRoleDetail(int roleId)
        {
            try
            {
                var role = roleRepository.RoleDetail(roleId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Role",
                    Data = role
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
