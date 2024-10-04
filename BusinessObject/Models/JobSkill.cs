using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class JobSkill
{
    public Guid JobSkillId { get; set; }

    public string? SkillName { get; set; }

    public virtual ICollection<AccountSkillsMapping> AccountSkillsMappings { get; set; } = new List<AccountSkillsMapping>();

    public virtual ICollection<JobSkillsMapping> JobSkillsMappings { get; set; } = new List<JobSkillsMapping>();
}
