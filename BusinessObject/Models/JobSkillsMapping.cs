using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class JobSkillsMapping
{
    public Guid JobId { get; set; }

    public Guid JobSkillId { get; set; }

    public string? ExperienceRequired { get; set; }

    public virtual Job Job { get; set; } = null!;

    public virtual JobSkill JobSkill { get; set; } = null!;
}
