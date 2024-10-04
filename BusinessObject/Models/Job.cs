using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Job
{
    public Guid JobId { get; set; }

    public string? JobTitle { get; set; }

    public string? JobDescription { get; set; }

    public string? JobRequirements { get; set; }

    public string? JobBenefits { get; set; }

    public string? JobTime { get; set; }

    public string? SalaryRange { get; set; }

    public string? ExperiencesRequired { get; set; }

    public DateOnly? Deadline { get; set; }

    public string? Status { get; set; }

    public DateOnly? DateCreated { get; set; }

    public Guid? RecruiterId { get; set; }

    public Guid? JobTypeId { get; set; }

    public Guid? JobCategoryId { get; set; }

    public Guid? JobLevelId { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual JobCategory? JobCategory { get; set; }

    public virtual JobLevel? JobLevel { get; set; }

    public virtual ICollection<JobSkillsMapping> JobSkillsMappings { get; set; } = new List<JobSkillsMapping>();

    public virtual JobType? JobType { get; set; }

    public virtual Account? Recruiter { get; set; }
}
