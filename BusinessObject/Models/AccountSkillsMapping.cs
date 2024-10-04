using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class AccountSkillsMapping
{
    public Guid AccountId { get; set; }

    public Guid JobSkillId { get; set; }

    public string? Experienced { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual JobSkill JobSkill { get; set; } = null!;
}
