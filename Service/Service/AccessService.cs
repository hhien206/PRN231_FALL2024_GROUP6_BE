using DataAccessObject.ViewModels;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AccessService : IAccessService
    {
        private readonly IAccessRepository _accessRepository;

        public AccessService(IAccessRepository accessRepository)
        {
            _accessRepository = accessRepository;
        }

        public async Task<AccountInfo?> Login(LoginModel model)
        {
            return await _accessRepository.Login(model);
        }

        public async Task<bool> Signup(SignupModel model)
        {
            return await _accessRepository.Signup(model);
        }
    }
}
