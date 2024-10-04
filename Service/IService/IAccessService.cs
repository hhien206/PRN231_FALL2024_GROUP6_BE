using DataAccessObject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAccessService
    {
        public Task<AccountInfo?> Login(LoginModel model);

        public Task<bool> Signup(SignupModel model);
    }
}
