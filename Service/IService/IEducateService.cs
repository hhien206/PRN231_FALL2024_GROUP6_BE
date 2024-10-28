using BusinessObject.AddModel;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IEducateService
    {
        public Task<ServiceResult> ViewAllEducateAccount(int accountId);
        public Task<ServiceResult> ViewDetailEducate(int educateId);
        public Task<ServiceResult> AddEducate(EducateAdd key);
        public Task<ServiceResult> DeleteEducate(int educateId);
    }
}
