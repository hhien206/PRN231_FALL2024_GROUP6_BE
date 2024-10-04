using AutoMapper;
using Repository.Helpers;
using BusinessObject.Models;
using DataAccessObject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using BC = global::BCrypt.Net.BCrypt;

namespace Repository.Repository
{

    public class AccessRepository : IAccessRepository
    {
        private readonly Prn231DbJobRecruitmentContext _dbContext;
        private readonly IMapper _mapper;

        public AccessRepository(Prn231DbJobRecruitmentContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AccountInfo?> Login(LoginModel model)
        {
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(user => user.Email == model.Email);

            if (user != null)
            {
                if (!BC.EnhancedVerify(model.Password, user.PasswordHash)) { return null; }
                var roleId = user.RoleId;
                if (roleId == null) return null;

                var role = await _dbContext.Roles.FirstOrDefaultAsync(role => role.RoleId == roleId);
                var userInfo = _mapper.Map<AccountInfo>(user);
                userInfo.Role = role?.RoleName;

                return userInfo;
            }

            return null;
        }

        public async Task<Boolean> Signup(SignupModel model)
        {
            try
            {
                var newUser = _mapper.Map<Account>(model);

                var role = await _dbContext.Roles.FirstOrDefaultAsync(role => role.RoleName == "Member");
                if (role == null) return false;
                newUser.RoleId = role?.RoleId;

                await _dbContext.Accounts.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();

                return true;

            }
                catch (Exception)
            {
                return false;
            }
        }
    }
}
