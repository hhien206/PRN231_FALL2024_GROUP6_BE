﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccessObject.Models;

public partial class Job
{
    public int JobId { get; set; }

    public string JobTitle { get; set; }

    public string Description { get; set; }

    public string Requirement { get; set; }

    public string Benefit { get; set; }

    public string JobTime { get; set; }

    public string SalaryRange { get; set; }

    public string Experience { get; set; }

    public DateTime? Deadline { get; set; }

    public string Status { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? AccountId { get; set; }

    public int? JobTypeId { get; set; }

    public int? JobLevelId { get; set; }

    public int? JobCategoryId { get; set; }

    public int? JobSkillId { get; set; }

    public virtual Account Account { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual JobCategory JobCategory { get; set; }

    public virtual ICollection<JobJobSkill> JobJobSkills { get; set; } = new List<JobJobSkill>();

    public virtual JobLevel JobLevel { get; set; }

    public virtual JobType JobType { get; set; }
}