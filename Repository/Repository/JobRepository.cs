﻿using BusinessObject.AddModel;
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
        IJobSkillRepository skillRepo;
        IJobCategoryRepository cateRepo;
        IJobLevelRepository levelRepo;
        IJobTypeRepository typeRepo;
        IAccountRepository accountRepo;
        public JobRepository()
        {
            jJSkillRepo = new JobJobSkillRepository();
            skillRepo = new JobSkillRepository();
            cateRepo = new JobCategoryRepository();
            levelRepo = new JobLevelRepository();
            typeRepo = new JobTypeRepository();
            accountRepo = new AccountRepository();
        }
        public async Task<List<JobView>> GetAllJob()
        {
            return await ConvertListJobToListJobView(await GetAllAsync());
        }
        public async Task<List<JobView>> GetAllJobByAccount(int accountId)
        {
            return await ConvertListJobToListJobView((await GetAllAsync()).FindAll(l=>l.AccountId == accountId));
        }
        public async Task<JobView?> AddJob(JobAdd key)
        {
            try
            {
                var job = new Job()
                {
                    JobTitle = key.JobTitle,
                    Description = key.Description,
                    UrlPicture = key.UrlPicture,
                    Requirement = key.Requirement,
                    Benefit = key.Benefit,
                    JobTime = key.JobTime,
                    Location = key.Location,
                    SalaryRange = key.SalaryRange,
                    Experience = key.Experience,
                    Deadline = key.Deadline,
                    QuantityMax = key.MaxQuantiy,
                    QuantityCurrent = 0,
                    JobCategoryId = key.JobCategoryId,
                    JobLevelId = key.JobLevelId,
                    JobTypeId = key.JobTypeId,
                    AccountId = key.AccountId
                };
                await CreateAsync(job);
                List<JobJobSkillView> jobSkills = new List<JobJobSkillView>();
                foreach (var item in key.listJobSkill)
                {
                    JobJobSkillView jobSkill = new();
                    jobSkill = await skillRepo.JobSkillViewDetail(item.JobId);
                    jobSkill.ExperienceRequirement = item.ExperienceRequirement;
                    jobSkills.Add(jobSkill);
                    await jJSkillRepo.CreateAsync(new JobJobSkill()
                    {
                        JobId = job.JobId,
                        JobSkillId = item.JobId,
                        ExperienceRequired = "1"
                    });
                }
                JobView jView = new();
                jView.ConvertJob(job, jobSkills,cateRepo.GetById(job.JobCategoryId),
                    levelRepo.GetById(job.JobLevelId), typeRepo.GetById(job.JobTypeId), await accountRepo.GetAccountById((int)job.AccountId));
                return jView;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task IncreaseCurrentQuantityByOne(int jobId)
        {
            try
            {
                var job = GetById(jobId);
                if (job == null) return;
                if (job.QuantityCurrent == null) return;
                job.QuantityCurrent += 1;
                await UpdateAsync(job);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> CheckQuantity(int jobId)
        {
            try
            {
                var job = GetById(jobId);
                if (job == null) return false;
                if (job.QuantityCurrent == null) return false;
                if (job.QuantityCurrent >= job.QuantityMax) return false;
                return true;
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
            if (key.jobCategoryId != null) result.FindAll(l=>l.JobCategoryId == key.jobCategoryId);
            if (key.jobLevelId != null) result.FindAll(l => l.JobLevelId == key.jobLevelId);
            if (key.jobTypeId != null) result.FindAll(l => l.JobTypeId == key.jobTypeId);
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
                jView.ConvertJob(job, await GetAllSkillOfJob(job),cateRepo.GetById(job.JobCategoryId),
                    levelRepo.GetById(job.JobLevelId), typeRepo.GetById(job.JobTypeId), await accountRepo.GetAccountById((int)job.AccountId));
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
                List<JobJobSkillView> listSkill = new();
                List<JobJobSkill> listjJSkill = (await jJSkillRepo.GetAllAsync()).FindAll(l => l.JobId == item.JobId);
                foreach (var item1 in listjJSkill)
                {
                    JobJobSkillView jobSkill = new();
                    jobSkill = await skillRepo.JobSkillViewDetail((int)item1.JobSkillId);
                    jobSkill.ExperienceRequirement = item1.ExperienceRequired;
                    listSkill.Add(jobSkill);
                }
                JobView jView = new();
                jView.ConvertJob(item, listSkill, cateRepo.GetById(item.JobCategoryId),
                    levelRepo.GetById(item.JobLevelId), typeRepo.GetById(item.JobTypeId), await accountRepo.GetAccountById((int)item.AccountId));
                result.Add(jView);
            }
            return result;
        }
        private async Task<List<JobJobSkillView>> GetAllSkillOfJob(Job job)
        {
            try
            {
                var jJobSkills = (await jJSkillRepo.GetAllAsync()).FindAll(l => l.JobId == job.JobId);
                List<JobJobSkillView> jobSkills = new();
                foreach (var item in jJobSkills)
                {
                    JobJobSkillView jobSkill = new JobJobSkillView();
                    jobSkill = await skillRepo.JobSkillViewDetail((int)item.JobSkillId);
                    jobSkill.ExperienceRequirement = item.ExperienceRequired;
                    jobSkills.Add(jobSkill);
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
