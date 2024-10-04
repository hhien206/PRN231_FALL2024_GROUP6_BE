using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Account
{
    public Guid AccountId { get; set; }

    public Guid? RoleId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? ProfilePicture { get; set; }

    public virtual ICollection<AccountSkillsMapping> AccountSkillsMappings { get; set; } = new List<AccountSkillsMapping>();

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();

    public virtual Role? Role { get; set; }
}
