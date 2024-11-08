using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.AddModel
{
    public class JobAdd
    {
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Job Title is required.")]
        [MaxLength(100, ErrorMessage = "Job Title cannot exceed 100 characters.")]
        public string? JobTitle { get; set; }
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? UrlPicture {  get; set; }
        [Required(ErrorMessage = "Description is required.")]

        public string? Description { get; set; }
        [Required(ErrorMessage = "Requirement is required.")]
        public string? Requirement { get; set; }
        [Required(ErrorMessage = "Benefit is required.")]
        public string? Benefit { get; set; }
        [Required(ErrorMessage = "Job Time is required.")]
        public string? JobTime { get; set; }
        [Required(ErrorMessage = "Location is required.")]
        public string? Location {  get; set; }

        [RegularExpression(@"^\$?(\d{1,3}(?:,\d{3})*)(\.\d{1,2})?$", ErrorMessage = "Please enter a valid salary range (e.g., $5000, $5,000.00).")]
        public string? SalaryRange { get; set; }
        [Required(ErrorMessage = "Experience is required.")]
        public string? Experience { get; set; }
        [Required(ErrorMessage = "Deadline is required.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime? Deadline { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Max Quantity must be at least 1.")]
        public int MaxQuantiy { get; set; }

        public List<JobJobSkillAdd>? listJobSkill { get; set; }
        public int? JobCategoryId { get; set; }
        public int? JobLevelId { get; set; }
        public int? JobTypeId { get; set; }


    }
}
