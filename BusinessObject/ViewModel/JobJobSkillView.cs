using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class JobJobSkillView
    {
        public int JobSkillId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        public string? ExperienceRequirement { get; set; }
        public void CovertJobSkillIntoJobJobSkillView(JobSkill jobSkill)
        {
            JobSkillId = jobSkill.Id;
            Name = jobSkill.Name;
            Description = jobSkill.Description;
        }
    }
}
