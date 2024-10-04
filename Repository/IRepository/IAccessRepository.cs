using DataAccessObject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAccessRepository
    {
        public Task<AccountInfo?> Login(LoginModel model);

        public Task<Boolean> Signup(SignupModel model);
    }
}
