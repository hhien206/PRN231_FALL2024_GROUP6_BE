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
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        IJobJobSkillRepository jJSkillRepo;
        IJobSkillRepository SkillRepo;
        public JobRepository()
        {
            jJSkillRepo = new JobJobSkillRepository();
            SkillRepo = new JobSkillRepository();
        }
        public async Task<JobView?> AddJob(JobAdd key)
        {
            try
            {
                var job = new Job()
                {
                    JobTitle = key.JobTitle,
                    Description = key.Description,
                    Requirement = key.Requirement,
                    Benefit = key.Benefit,
                    JobTime = key.JobTime,
                    SalaryRange = key.SalaryRange,
                    Experience = key.Experience,
                    Deadline = key.Deadline,
                };
                await CreateAsync(job);
                List<Jobkill> jobSkills = new List<Jobkill>();
                foreach (var item in key.listJobSkillId)
                {
                    jobSkills.Add(SkillRepo.GetById(item));
                    await jJSkillRepo.CreateAsync(new JobJobSkill()
                    {
                        JobId = job.JobId,
                        JobSkillId = item
                    });
                }
                JobView jView = new();
                jView.ConvertJob(job, jobSkills);
                return jView;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<List<Job>?> JobSearch(JobSearch key)
        {
            var jJobSkills = await jJSkillRepo.GetAllAsync();
            if (key.listJobSkillIdInclude != null && key.listJobSkillIdInclude.Count > 0)
            {
                foreach (var item in key.listJobSkillIdInclude)
                {
                    var listTemp = new List<JobJobSkill>();
                    listTemp.AddRange(jJobSkills.FindAll(l => l.JobSkillId == item));
                    var listTemp1 = new List<JobJobSkill>();
                    foreach (var item1 in listTemp)
                    {
                        listTemp1.AddRange(jJobSkills.FindAll(l => l.JobId == item1.JobId));
                    }
                    jJobSkills = listTemp1;
                }
            }
            if (key.listJobSkillIdExclude != null && key.listJobSkillIdExclude.Count > 0)
            {
                foreach (var item in key.listJobSkillIdExclude)
                {
                    var listTemp = new List<JobJobSkill>();
                    listTemp.AddRange(jJobSkills.FindAll(l => l.JobSkillId == item));
                    foreach (var item1 in listTemp)
                    {
                        jJobSkills.RemoveAll(l => l.JobId == item1.JobId);
                    }
                }
            }
            var jobs = (await GetAllAsync()).FindAll(l => l.JobTitle.ToLower().Contains(key.name.ToLower()));
            List<Job> result = new List<Job>();
            foreach (var item in jJobSkills)
            {
                var job = jobs.SingleOrDefault(l => l.JobId == item.JobId);
                if (job == null) continue;
                jobs.Remove(job);
                result.Add(job);
            }
            return result;
        }
        public async Task<List<JobView>?> ViewListJob(int sizePaging, int indexPaging, JobSearch key)
        {
            try
            {
                var jobs = await JobSearch(key);
                jobs = Paging(jobs, sizePaging, indexPaging);


                return await ConvertListJobToListJobView(jobs);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JobView?> ViewJobDetail(int jobId)
        {
            try
            {
                var job = GetById(jobId);
                JobView jView = new();
                jView.ConvertJob(job, await GetAllSkillOfJob(job));
                return jView;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task<List<JobView>> ConvertListJobToListJobView(List<Job> listJobs)
        {
            List<JobView> result = new List<JobView>();
            foreach (var item in listJobs)
            {
                List<Jobkill> listSkill = new();
                List<JobJobSkill> listjJSkill = (await jJSkillRepo.GetAllAsync()).FindAll(l => l.JobId == item.JobId);
                foreach (var item1 in listjJSkill)
                {
                    listSkill.Add(SkillRepo.GetById((int)item1.JobSkillId));
                }
                JobView jView = new();
                jView.ConvertJob(item, listSkill);
                result.Add(jView);
            }
            return result;
        }
        private async Task<List<Jobkill>> GetAllSkillOfJob(Job job)
        {
            try
            {
                var jJobSkills = (await jJSkillRepo.GetAllAsync()).FindAll(l => l.JobId == job.JobId);
                List<Jobkill> jobSkills = new();
                foreach (var item in jJobSkills)
                {
                    jobSkills.Add(SkillRepo.GetById((int)item.Id));
                }
                return jobSkills;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
