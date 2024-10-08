using BusinessObject.ViewModel;
using DataAccessObject.Models;
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
    public class JobSkillService : IJobSkillService
    {
        public IJobSkillRepository skillRepository;
        public JobSkillService()
        {
            this.skillRepository = new JobSkillRepository();
        }
        public async Task<ServiceResult> ViewAllJobSkill(string? nameQuickSearch)
        {
            try
            {
                var listJobSkill = await skillRepository.ListJobSkillQuickSearch(nameQuickSearch);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"List Skill With Search '{nameQuickSearch}'",
                    Data = listJobSkill
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
        public async Task<ServiceResult> ViewDetailJobSkill(int jobSkillId)
        {
            try
            {
                var jobSkill = skillRepository.GetById(jobSkillId);
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Job Skill",
                    Data = jobSkill
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
        public async Task<ServiceResult> AddJobSkill(JobSkillAdd key)
        {
            try
            {
                var skill = new JobSkill
                {
                    Name = key.Name,
                    Description = key.Description,
                };
                await skillRepository.CreateAsync(skill);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Skill Success",
                    Data = skill
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
        public async Task<ServiceResult> UpdateJobSkill(int jobSkillId, JobSkillUpdate key)
        {
            try
            {
                var skill = skillRepository.GetById(jobSkillId);
                if (skill == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Skill Not Found",
                    };
                }
                skill.Name = key.Name;
                skill.Description = key.Description;
                await skillRepository.UpdateAsync(skill);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Skill Success",
                    Data = skill
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
        public async Task<ServiceResult> DeleteJobSkill(int jobSkillId)
        {
            try
            {
                var skill = skillRepository.GetById(jobSkillId);
                if (skill == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Skill Not Found",
                    };
                }
                await skillRepository.RemoveAsync(skill);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Delete Success",
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
