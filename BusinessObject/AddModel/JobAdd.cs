using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.AddModel
{
    public class JobAdd
    {
        public int AccountId { get; set; }
        public string? JobTitle { get; set; }
        public string? UrlPicture {  get; set; }

        public string? Description { get; set; }

        public string? Requirement { get; set; }

        public string? Benefit { get; set; }

        public string? JobTime { get; set; }
        public string? Location {  get; set; }

        public string? SalaryRange { get; set; }

        public string? Experience { get; set; }

        public DateTime? Deadline { get; set; }
        public int MaxQuantiy { get; set; }

        public List<JobJobSkillAdd>? listJobSkill { get; set; }
        public int? JobCategoryId { get; set; }
        public int? JobLevelId { get; set; }
        public int? JobTypeId { get; set; }


    }
}
