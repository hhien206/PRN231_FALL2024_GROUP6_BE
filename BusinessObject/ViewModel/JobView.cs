using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class JobView
    {
        [Key]
        public int JobId { get; set; }
        public string JobTitle { get; set; }

        public string Description { get; set; }

        public string Requirement { get; set; }

        public string Benefit { get; set; }

        public string JobTime { get; set; }
        public string Location { get; set; }

        public string SalaryRange { get; set; }

        public string Experience { get; set; }

        public DateTime? Deadline { get; set; }
        public List<JobJobSkillView> ListJobSkill { get; set; }
        public JobCategory JobCategory { get; set; }
        public JobLevel JobLevel { get; set; }
        public JobType JobType { get; set; }
        public AccountView Account { get; set; }
        public void ConvertJob(Job job, List<JobJobSkillView> listJobSkill, JobCategory jobCategory,
            JobLevel jobLevel,JobType jobType, AccountView account)
        {
            JobId = job.JobId;
            JobTitle = job.JobTitle;
            Description = job.Description;
            Requirement = job.Requirement;
            Benefit = job.Benefit;
            Location = job.Location;
            JobTime = job.JobTime;
            SalaryRange = job.SalaryRange;
            Experience = job.Experience;
            Deadline = job.Deadline;
            ListJobSkill = listJobSkill;
            this.JobCategory = jobCategory;
            this.JobLevel = jobLevel;
            this.JobType = jobType;
            this.Account = account;
        }
    }
}
