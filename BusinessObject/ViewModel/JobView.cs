using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class JobView
    {
        public string JobTitle { get; set; }

        public string Description { get; set; }

        public string Requirement { get; set; }

        public string Benefit { get; set; }

        public string JobTime { get; set; }

        public string SalaryRange { get; set; }

        public string Experience { get; set; }

        public DateTime? Deadline { get; set; }
        public List<Jobkill> ListJobSkill { get; set; }
        public void ConvertJob(Job job, List<Jobkill> listJobSkill)
        {
            JobTitle = job.JobTitle;
            Description = job.Description;
            Requirement = job.Requirement;
            Benefit = job.Benefit;
            JobTime = job.JobTime;
            SalaryRange = job.SalaryRange;
            Experience = job.Experience;
            Deadline = job.Deadline;
            ListJobSkill = listJobSkill;
        }
    }
}
